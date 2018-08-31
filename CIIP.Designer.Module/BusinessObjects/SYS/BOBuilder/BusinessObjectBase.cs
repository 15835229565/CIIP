using System;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CIIP.Persistent.BaseImpl;
using System.Collections.Generic;
using System.Linq;

namespace CIIP.Module.BusinessObjects.SYS
{

    [XafDisplayName("����")]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    public class BusinessObjectBase : NameObject, ICategorizedItem
    {
        private bool _IsRuntimeDefine;

        [XafDisplayName("��̬����")]
        [ToolTip("Ϊ��ʱ��ͨ�����뷽ʽ�ϴ���ģ�����ɵġ��������ڽ����϶��岢���ɵġ�")]
        public bool IsRuntimeDefine
        {
            get { return _IsRuntimeDefine; }
            set { SetPropertyValue("IsRuntimeDefine", ref _IsRuntimeDefine, value); }
        }
        private string _FullName;

        [ModelDefault("AllowEdit", "False")]
        [RuleRequiredField]
        //[Browsable(false)]
        [Size(-1)]
        public string FullName
        {
            get { return _FullName; }
            set { SetPropertyValue("FullName", ref _FullName, value); }
        }

        private Namespace _Category;
        [XafDisplayName("����")]
        [RuleRequiredField]
        public Namespace Category
        {
            get { return _Category; }
            set { SetPropertyValue("Category", ref _Category, value); }
        }

        private string _Caption;
        [XafDisplayName("����")]
        public string Caption
        {
            get { return _Caption; }
            set { SetPropertyValue("Caption", ref _Caption, value); }
        }

        private string _Description;
        [XafDisplayName("˵��"),Size(-1)]
        public string Description
        {
            get { return _Description; }
            set { SetPropertyValue("Description", ref _Description, value); }
        }

        [XafDisplayName("����ӿ�")]
        [Association, DevExpress.Xpo.Aggregated]
        public XPCollection<ImplementRelation> Implements
        {
            get
            {
                return GetCollection<ImplementRelation>(nameof(Implements));
            }
        }

        #region ���Ͳ�������

        #region ����
        [Persistent("IsGenericTypeDefine")]
        bool _isGenericTypeDefine;

        [XafDisplayName("���Ͷ���")]
        [ToolTip("�����Ƿ��Ƿ��Ͷ���")]
        [PersistentAlias("_isGenericTypeDefine")]
        public bool IsGenericTypeDefine
        {
            get
            {
                return _isGenericTypeDefine;
            }
        }

        #endregion

        [XafDisplayName("���Ͳ�������")]
        [ToolTip("�����Ҫ���Ͳ���ʱ,�����ڴ˶���,���������Լ�ҵ���߼���ʹ��!")]
        [Association, DevExpress.Xpo.Aggregated]
        public XPCollection<GenericParameterDefine> GenericParameterDefines
        {
            get
            {
                return GetCollection<GenericParameterDefine>(nameof(GenericParameterDefines));
            }
        }
        #endregion

        [Association, DevExpress.Xpo.Aggregated,XafDisplayName("����")]
        public XPCollection<PropertyBase> Properties
        {
            get
            {
                return GetCollection<PropertyBase>(nameof(Properties));
            }
        }

        ITreeNode ICategorizedItem.Category
        {
            get { return Category; }
            set { Category = (Namespace)value; }
        }

        public BusinessObjectBase(Session s) : base(s)
        {
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if(propertyName == nameof(GenericParameterDefines))
            {

            }
        }

        protected override void OnSaving()
        {
            _isGenericTypeDefine = GenericParameterDefines.Count > 0;
            base.OnSaving();
        }


    }
}