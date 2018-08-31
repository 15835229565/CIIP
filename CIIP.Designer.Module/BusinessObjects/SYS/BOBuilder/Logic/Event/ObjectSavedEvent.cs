using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CIIP.Module.BusinessObjects.SYS.Logic
{
    [XafDisplayName("�������")]
    public class ObjectSavedEvent : BusinessObjectEvent
    {
        public ObjectSavedEvent(Session s):base(s)
        {
            
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.CodeName = "OnSaved";
            this.AccessorModifier = AccessorModifier.Protected;
            this.MethodModifier = MethodModifier.Override;
            this.SetCode("base.OnSaved();");
        }
        

        public override string Name
        {
            get
            {
                return "���󱣴����";
            }

            set
            {
                base.Name = value;
            }
        }
    }
}