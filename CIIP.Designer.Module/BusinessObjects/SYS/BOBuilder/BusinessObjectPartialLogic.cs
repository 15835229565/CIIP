using DevExpress.ExpressApp.DC;
using DevExpress.Xpo;

namespace CIIP.Module.BusinessObjects.SYS
{
    [XafDisplayName("�ֲ��߼�")]
    public class BusinessObjectPartialLogic : BusinessObjectPartialLogicBase
    {
        public BusinessObjectPartialLogic(Session s) : base(s)
        {

        }

        public override string Template
        {
            get
            {
                return
                    $@"namespace {this.BusinessObject.Category.FullName}
{{
    public partial class {this.BusinessObject.����}
    {{

    }}
}}
";
            }
        }
    }
}