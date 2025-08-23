using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WfunCharp.CustomEnt.Arch;
using WfunCharp.CustomEnt.Arch.EntityExtent;
namespace WfunCharpCustomEntArch_Sample
{
    public class WfunTestCharpCustomEnt : EntCharpCustomEntBase
    {
        private Point3d m_ptCenter = Point3d.Origin;
        private double m_radius = 100;
        public WfunTestCharpCustomEnt()
        {

        }
        public WfunTestCharpCustomEnt(IntPtr unmanagedObjPtr, bool autoDelete = true)
            : base(unmanagedObjPtr, autoDelete)
        {

        }
        public Point3d Center { get => m_ptCenter; set => m_ptCenter = value; }
        public double Radius { get => m_radius; set => m_radius = value; }
        /// <summary>
        /// 数据读取
        /// </summary>
        /// <param name="data"></param>
        protected override void SubDataIn(DataIO data)
        {
            base.SubDataIn(data); 
            DataIO dataThis = data.GetData(); 
            dataThis.GetData(ref m_ptCenter);
            dataThis.GetData(ref m_radius);
        }
        /// <summary>
        /// 数据归档
        /// </summary>
        /// <param name="data"></param>
        protected override void SubDataOut(DataIO data)
        {
            base.SubDataOut(data);
            DataIO dataThis = new DataIO();
            dataThis.SetData(m_ptCenter);
            dataThis.SetData(m_radius);
            data.SetData(dataThis);
        }
        /// <summary>
        /// 实体绘制
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        protected override bool SubDraw(DrawEnt mode)
        {
            mode.Draw(new Circle() { Center = m_ptCenter, Radius = m_radius });

            return true;

        }
        /// <summary>
        /// 显示的实体夹点
        /// </summary>
        /// <param name="gripPts"></param>
        /// <returns></returns>
        protected override bool SubEntGetGripPoints(List<Point3d> gripPts)
        {
            gripPts.Add(m_ptCenter);
            return true;

        }
        /// <summary>
        /// 修改实体夹点
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        protected override bool SubEntSetGripPoints(Vector3d offset, int nIndex)
        {
            if (0 == nIndex)
                m_ptCenter = m_ptCenter + offset;

            return true;
        }
        /// <summary>
        /// 命令行显示属性
        /// </summary>
        protected override void SubEntList()
        {
            CadUnits.CurEdit().WriteMessage($"\n圆心:{m_ptCenter}");
            CadUnits.CurEdit().WriteMessage($"\n半径:{m_radius}");

        }
        /// <summary>
        /// 矩阵操作
        /// </summary>
        /// <param name="xform"></param>
        /// <returns></returns>
        protected override bool SubEntTransformBy(Matrix3d xform)
        {
            m_ptCenter = m_ptCenter.TransformBy(xform);
            return true;
        }
        /// <summary>
        /// 获取捕捉点
        /// </summary>
        /// <param name="osnapMode">捕捉点类型</param>
        /// <param name="gsSelectionMark">标记</param>
        /// <param name="pickPoint">拾取点</param>
        /// <param name="lastPoint">上一个点</param>
        /// <param name="viewXform">视口矩阵</param>
        /// <param name="snapPoints">返回捕捉点</param>
        /// <param name="geomIds">返回几何id？</param>
        /// <returns>true:采用本函数中的捕捉点规则，false：采用默认规则</returns>
        protected override bool SubEntGetOsnapPoints(ObjectSnapModes osnapMode, int gsSelectionMark,
            Point3d pickPoint, Point3d lastPoint, Matrix3d viewXform, List<Point3d> snapPoints, List<int> geomIds)
        {
            Circle c = new Circle();
            c.Center = m_ptCenter;
            c.Radius = m_radius;

            switch (osnapMode)
            {
                case ObjectSnapModes.ModeCenter:
                    {
                        // 圆心
                        snapPoints.Add(m_ptCenter);
                        break;
                    }
                case ObjectSnapModes.ModeNear:
                    {
                        // 最近点
                        var pt = c.GetClosestPointTo(pickPoint, false);
                        snapPoints.Add(pt);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }



            return true;
        }
    }
}
