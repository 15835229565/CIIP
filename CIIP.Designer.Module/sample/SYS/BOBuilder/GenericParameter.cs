using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CIIP.Module.BusinessObjects.SYS
{
    public class GenericParameter : BaseObject
    {
        public GenericParameter(Session s) : base(s)
        {

        }

        private BusinessObject _Owner;

        [Association]
        [XafDisplayName("����ҵ�����")]
        public BusinessObject Owner
        {
            get { return _Owner; }
            set { SetPropertyValue("Owner", ref _Owner, value); }
        }

        private string _Name;
        [XafDisplayName("��������")]
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue("Name", ref _Name, value); }
        }

        private int _ParameterIndex;
        [XafDisplayName("����˳��")]
        public int ParameterIndex
        {
            get { return _ParameterIndex; }
            set { SetPropertyValue("ParameterIndex", ref _ParameterIndex, value); }
        }

        private BusinessObjectBase _ParameterValue;
        [XafDisplayName("����")]
        [RuleRequiredField]
        [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
        public BusinessObjectBase ParameterValue
        {
            get { return _ParameterValue; }
            set { SetPropertyValue("ParameterValue", ref _ParameterValue, value); }
        }

        private BusinessObject _DefaultGenericType;
        [ToolTip("������Ϊ��������ʱ,��Ϊ����ʹ��ʱ,Ĭ����������Щ�ӱ�����.")]
        [XafDisplayName("Ĭ������")]
        public BusinessObject DefaultGenericType
        {
            get { return _DefaultGenericType; }
            set { SetPropertyValue("DefaultGenericType", ref _DefaultGenericType, value); }
        }
    }
}