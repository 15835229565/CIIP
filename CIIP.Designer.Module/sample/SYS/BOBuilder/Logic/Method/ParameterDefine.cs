using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace CIIP.Module.BusinessObjects.SYS.Logic
{
    [XafDisplayName("��������")]
    public class ParameterDefine : BaseObject
    {
        public ParameterDefine(Session s) : base(s)
        {

        }

        private MethodDefine _OwnerMethod;
        [XafDisplayName("��������"),Association]
        public MethodDefine OwnerMethod
        {
            get { return _OwnerMethod; }
            set { SetPropertyValue("OwnerMethod", ref _OwnerMethod, value); }
        }


        private string _ParameterType;
        [XafDisplayName("��������")]
        public string ParameterType
        {
            get { return _ParameterType; }
            set { SetPropertyValue("ParameterType", ref _ParameterType, value); }
        }

        private string _ParameterName;
        [XafDisplayName("��������")]
        public string ParameterName
        {
            get { return _ParameterName; }
            set { SetPropertyValue("ParameterName", ref _ParameterName, value); }
        }

        private int _Index;
        public int Index
        {
            get { return _Index; }
            set { SetPropertyValue("Index", ref _Index, value); }
        }



    }
}