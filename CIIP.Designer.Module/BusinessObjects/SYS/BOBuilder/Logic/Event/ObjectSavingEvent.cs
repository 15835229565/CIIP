using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CIIP.Module.BusinessObjects.SYS.Logic
{
    [XafDisplayName("���ڱ���")]
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
            this.SetCode("base.OnSaving();");
        }
        
        public override string Name
        {
            get { return "���󱣴�"; }

            set
            {
                base.Name = value;
            }
        }
    }
}