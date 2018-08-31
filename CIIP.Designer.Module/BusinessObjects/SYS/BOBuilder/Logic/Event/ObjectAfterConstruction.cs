using System;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CIIP.Module.BusinessObjects.SYS.Logic
{
    [XafDisplayName("���󴴽�")]
    public class ObjectAfterConstruction : BusinessObjectEvent
    {
        public ObjectAfterConstruction(Session s) : base(s)
        {
            
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.CodeName = "AfterConstruction";
            this.AccessorModifier = AccessorModifier.Public;
            this.MethodModifier = MethodModifier.Override;
            this.SetCode("base.AfterConstruction();");
        }

        public override string Name 
        {
            get { return "���󴴽�"; }

            set { base.Name = value; }
        }
        
    }

    //Ŀ��:���¼������ϸ��ͼ����ͳһ�Ű�

    //public class MethodDefine_ListView : ListViewObject<MethodDefine>
    //{
    //    public override void LayoutListView()
    //    {

    //    }

    //    public override void LayoutDetailView()
    //    {
    //        DetailViewLayout.ClearNodes();
    //        this.HGroup(10, x => x.Code);
    //        base.LayoutDetailView();
    //    }
    //}
}