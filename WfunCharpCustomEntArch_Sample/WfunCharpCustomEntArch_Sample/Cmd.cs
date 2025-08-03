using System;
using Autodesk.AutoCAD.Runtime;
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
            ent.UpDataFromCharp();
            CadUnits.AppendEntity(ent);
        }
        [CommandMethod("WfunCharpCustomEntArch_Sample_TestSelect")]
        public static void WfunCharpCustomEntArch_Sample_TestSelect()
        {
            var filter = RegEntArch.BuileFilterArch(new Type[]
            { 
                typeof(WfunTestCharpCustomEnt)
            });

            var ents = CadUnits.CurEdit().GetSelection(filter);

            if (ents.Value.Count == 0)
                return;

            using(var ts = CadUnits.StartTransaction())
            {
                var id = ents.Value[0].ObjectId;
                var entGet = RegEntArch.CreateEntArch(id, ts, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead);

                if (entGet == null)
                    return;
                WfunTestCharpCustomEnt ent = entGet as WfunTestCharpCustomEnt;
            }

        }
    }
}
