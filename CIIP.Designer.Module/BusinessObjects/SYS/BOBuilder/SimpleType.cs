using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CIIP.Module.BusinessObjects.SYS
{
    [XafDisplayName("������")]
    [DefaultClassOptions]
    public class SimpleType : BusinessObjectBase
    {
        public SimpleType(Session s) : base(s)
        {
        }
    }
}