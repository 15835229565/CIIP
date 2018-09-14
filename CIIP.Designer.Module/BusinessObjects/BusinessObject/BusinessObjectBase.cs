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
using DevExpress.Persistent.BaseImpl;

namespace CIIP.Designer
{
    [NavigationItem]
    public class Order : BaseObject
    {
        public Order(Session session) : base(session)
        {
        }
        public string Code
        {
            get { return GetPropertyValue<string>(nameof(Code)); }
            set { SetPropertyValue(nameof(Code), value); }
        }


        public string Test
        {
            get
            {
                return "";
            }
        }

        //[PersistentAlias("[Items]")]
        //public XPCollection<OrderItem> Items2
        //{
        //    get
        //    {
        //        var t = EvaluateAlias(nameof(Items2));
        //        return (XPCollection<OrderItem>)t;
        //    }
        //}


        [Association, DevExpress.Xpo.Aggregated]
        public XPCollection<OrderItem> Items
        {
            get
            {
                return GetCollection<OrderItem>(nameof(Items));
            }
        }
    }

    public class OrderItem : BaseObject
    {
        public OrderItem(Session session) : base(session)
        {
        }
        [Association]
        public Order Order
        {
            get { return GetPropertyValue<Order>(nameof(Order)); }
            set { SetPropertyValue(nameof(Order), value); }
        }

        public string Product
        {
            get { return GetPropertyValue<string>(nameof(Product)); }
            set { SetPropertyValue(nameof(Product), value); }
        }
    }

    [XafDisplayName("����")]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    public class BusinessObjectBase : NameObject, ICategorizedItem
    {
        public PropertyBase FindProperty(string name)
        {
            return Properties.SingleOrDefault(x => x.Name == name);
        }
        #region modifier
        [XafDisplayName("�̳�����")]
        [ToolTip("��������Ϊ��,����,�ܷ��")]
        public virtual BusinessObjectModifier DomainObjectModifier
        {
            get
            {
                return GetPropertyValue<BusinessObjectModifier>(nameof(DomainObjectModifier));
            }
            set
            {
                SetPropertyValue(nameof(DomainObjectModifier), value);
            }
        }
        #endregion

        [XafDisplayName("����ģ��"),Association]
        public BusinessModule BusinessModule
        {
            get { return GetPropertyValue<BusinessModule>(nameof(BusinessModule)); }
            set { SetPropertyValue(nameof(BusinessModule), value); }
        }


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
        //[RuleRequiredField]
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