using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CIIP.Module.BusinessObjects.SYS.Logic
{
    [XafDisplayName("����ɾ��")]
    public class ObjectDeletingEvent : BusinessObjectEvent
    {
        public ObjectDeletingEvent(Session s) : base(s)
        {

        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.CodeName = "OnDeleting";
            this.AccessorModifier = AccessorModifier.Protected;
            this.MethodModifier = MethodModifier.Override;
            this.SetCode("base.OnDeleting();");
        }
        
        public override string Name
        {
            get
            {
                return "ɾ������";
            }

            set
            {
                base.Name = value;
            }
        }
    }
}