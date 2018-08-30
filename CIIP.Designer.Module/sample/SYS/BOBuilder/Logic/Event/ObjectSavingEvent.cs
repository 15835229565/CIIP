using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CIIP.Module.BusinessObjects.SYS.Logic
{
    [XafDisplayName("���ڱ���")]
    [CreatableItem]
    public class ObjectSavingEvent : BusinessObjectEvent
    {
        public ObjectSavingEvent(Session s) : base(s)
        {

        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.CodeName = "OnSaving";
            this.AccessorModifier = AccessorModifier.Protected;
            this.MethodModifier = MethodModifier.Override;
            this.Code = "base.OnSaving();";
        }
        
        public override string ����
        {
            get { return "���󱣴�"; }

            set
            {
                base.���� = value;
            }
        }
    }
}