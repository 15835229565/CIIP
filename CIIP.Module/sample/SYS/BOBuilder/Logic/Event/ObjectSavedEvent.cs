using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CIIP.Module.BusinessObjects.SYS.Logic
{
    [XafDisplayName("�������")]
    [CreatableItem]
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
            this.Code = "base.OnSaved();";
        }
        

        public override string ����
        {
            get
            {
                return "���󱣴����";
            }

            set
            {
                base.���� = value;
            }
        }
    }
}