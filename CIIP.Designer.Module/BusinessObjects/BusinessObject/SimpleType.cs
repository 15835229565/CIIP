using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CIIP.Designer
{
    [XafDisplayName("������")]
    [DefaultClassOptions]
    public class SimpleType : BusinessObjectBase
    {
        public SimpleType(Session s) : base(s)
        {
        }

        public override BusinessObjectModifier DomainObjectModifier { get => Designer.BusinessObjectModifier.Sealed; set { } }
    }
}