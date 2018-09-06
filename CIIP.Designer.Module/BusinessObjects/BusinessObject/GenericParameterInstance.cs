using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CIIP.Designer
{
    /// <summary>
    /// ���Ͳ���ʵ��
    /// �����������д���������Ҫ�ķ��Ͳ���
    /// </summary>
    public class GenericParameterInstance : BaseObject
    {
        public GenericParameterInstance(Session session) : base(session)
        {
        }

        //public class ClassName<TItem,MyT> : Form<TItem,�ͻ�����>
        //where MyT : class|interface|����|�ӿ�|enum|delegate ��.

        private ImplementRelation _Owner;

        [Association]
        [XafDisplayName("����ҵ�����")]
        [ToolTip("ָ�������ʵ�������ĸ�ҵ������ж����")]
        public ImplementRelation Owner
        {
            get { return _Owner; }
            set { SetPropertyValue("Owner", ref _Owner, value); }
        }

        [XafDisplayName("���ղ�������")]
        [ToolTip("�����ǻ���,Ҳ���Դ����̳еĽӿ�")]
        public BusinessObject TargetBusinessObject
        {
            get { return GetPropertyValue<BusinessObject>(nameof(TargetBusinessObject)); }
            set { SetPropertyValue(nameof(TargetBusinessObject), value); }
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

        //protected bool ParameterValueMutsBeNotNull
        //{
        //    get
        //    {
        //        if (Owner == null)
        //            return true;
        //        return !Owner.IsGenericTypeDefine;
        //    }
        //}

        private BusinessObjectBase _ParameterValue;
        [XafDisplayName("����")]
        //[RuleRequiredField(TargetCriteria = "ParameterValueMutsBeNotNull")]
        [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
        public BusinessObjectBase ParameterValue
        {
            get { return _ParameterValue; }
            set { SetPropertyValue("ParameterValue", ref _ParameterValue, value); }
        }

        private BusinessObject _DefaultGenericType;

#warning �����ǲ������,��Ҫ�Ż�
        [ToolTip("������Ϊ��������ʱ,��Ϊ����ʹ��ʱ,Ĭ����������Щ�ӱ�����.")]
        [XafDisplayName("Ĭ������")]
        public BusinessObject DefaultGenericType
        {
            get { return _DefaultGenericType; }
            set { SetPropertyValue("DefaultGenericType", ref _DefaultGenericType, value); }
        }
    }

}