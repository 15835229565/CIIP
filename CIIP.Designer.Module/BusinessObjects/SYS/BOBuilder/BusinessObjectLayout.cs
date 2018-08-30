using DevExpress.ExpressApp.DC;
using DevExpress.Xpo;

namespace CIIP.Module.BusinessObjects.SYS
{
    [XafDisplayName("����")]
    public class BusinessObjectLayout : BusinessObjectPartialLogicBase
    {
        public BusinessObjectLayout(Session s):base(s)
        {
            
        }
        
        public override string Template
        {
            get
            {
                return
                    $@"namespace {this.BusinessObject.Category.FullName}
{{
    public partial class {this.BusinessObject.����}_ListView : <<BaseClass>>
    {{

    }}
}}
";
            }
        }


        public override string GetFileName()
        {
            return this.BusinessObject.FullName + "_ListView";
        }

    }
}