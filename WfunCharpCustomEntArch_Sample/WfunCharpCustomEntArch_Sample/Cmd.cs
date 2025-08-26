using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;
using WfunCharp.CustomEnt.Arch;
using WfunCharp.CustomEnt.Arch.EntityArch;
using WfunCharp.CustomEnt.Arch.EntityExtent;
namespace WfunCharpCustomEntArch_Sample
{
    public class Cmd : IExtensionApplication
    {
        public void Initialize()
        {
            // 注册
            RegEntArch.Register<WfunTestCharpCustomEnt>(
                (unmanagedObjPtr, autoDelete) => 
                new WfunTestCharpCustomEnt(unmanagedObjPtr, autoDelete));
        }

        public void Terminate()
        {
        }
        /// <summary>
        /// 测试命令
        /// </summary>
        [CommandMethod("WfunCharpCustomEntArch_Sample_Test")]
        public static void WfunCharpCustomEntArch_Sample_Test()
        {
            WfunTestCharpCustomEnt ent = new WfunTestCharpCustomEnt();
            ent.UpDataFromCharp(); // 必须调用，否则C#修改的数据更新不到实体上
            CadUnits.AppendEntity(ent);
            Line line = new Line(Point3d.Origin, Point3d.Origin + Vector3d.XAxis * 200);

            Point3dCollection pts = new Point3dCollection();
            ent.IntersectWith(line, Intersect.OnBothOperands, pts, IntPtr.Zero, IntPtr.Zero);
        }
        [CommandMethod("WfunCharpCustomEntArch_Sample_TestSelect")]
        public static void WfunCharpCustomEntArch_Sample_TestSelect()
        {
            var filter = RegEntArch.BuileFilterArch(new Type[]
            { 
                typeof(WfunTestCharpCustomEnt)
            });

            var ents = CadUnits.CurEdit().GetSelection(filter);

            if (null == ents || ents.Value == null || ents.Value.Count == 0)
                return;

            using(var ts = CadUnits.StartTransaction())
            {
                var id = ents.Value[0].ObjectId;
                var entGet = RegEntArch.CreateEntArch(id, ts, OpenMode.ForWrite);

                if (entGet == null)
                    return;
                WfunTestCharpCustomEnt ent = entGet as WfunTestCharpCustomEnt;
                ent.ColorIndex = 1;
                ent.Radius = 1000;

                ent.UpDataFromCharp();// 必须调用，否则C#修改的数据更新不到实体上

                ts.Commit();
            }

        }
    }
}
