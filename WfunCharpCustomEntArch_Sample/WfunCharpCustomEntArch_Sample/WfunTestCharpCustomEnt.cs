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
            :base()
        {

        }
        public WfunTestCharpCustomEnt(IntPtr unmanagedObjPtr)
            : base(unmanagedObjPtr)
        {

        }
        public WfunTestCharpCustomEnt(IntPtr unmanagedObjPtr, bool autoDelete)
            : base(unmanagedObjPtr, autoDelete)
        {

        }
        protected override void SubDataIn(DataIO data)
        {
            base.SubDataIn(data); 
            DataIO dataThis = data.GetData(); 
            dataThis.GetData(ref m_ptCenter);
            dataThis.GetData(ref m_radius);

        }
        protected override void SubDataOut(DataIO data)
        {
            base.SubDataOut(data);
            DataIO dataThis = new DataIO();
            dataThis.SetData(m_ptCenter);
            dataThis.SetData(m_radius);
            data.SetData(dataThis);

        }
        protected override bool SubDraw(DrawEnt mode)
        {
            mode.Draw(new Circle() { Center = m_ptCenter, Radius = m_radius });

            return true;

        }
        protected override bool SubEntGetGripPoints(List<Point3d> gripPts)
        {
            gripPts.Add(m_ptCenter);
            return true;

        }
        protected override bool SubEntSetGripPoints(Vector3d offset, int nIndex)
        {
            if (0 == nIndex)
                m_ptCenter = m_ptCenter + offset;

            return true;
        }
        protected override void SubEntList()
        {
            CadUnits.CurEdit().WriteMessage($"\n圆心:{m_ptCenter}");
            CadUnits.CurEdit().WriteMessage($"\n半径:{m_radius}");

        }
        protected override bool SubEntTransformBy(Matrix3d xform)
        {
            m_ptCenter = m_ptCenter.TransformBy(xform);
            return true;
        }
    }
}
