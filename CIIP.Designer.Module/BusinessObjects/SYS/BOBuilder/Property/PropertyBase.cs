using System.Collections.Generic;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using CIIP.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using System.Linq;

namespace CIIP.Module.BusinessObjects.SYS
{
    [XafDefaultProperty("DisplayName")]    
    public abstract class PropertyBase : NameObject
    {
        #region ����ҵ��
        [Association]
        public BusinessObjectBase BusinessObject
        {
            get
            {
                return GetPropertyValue<BusinessObjectBase>(nameof(BusinessObject));
            }
            set
            {
                SetPropertyValue(nameof(BusinessObject), value);
            }
        } 
        #endregion

        #region ����
        private BusinessObjectBase _PropertyType;

        [XafDisplayName("����"), RuleRequiredField]
        [ImmediatePostData]
        [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
        public BusinessObjectBase PropertyType
        {
            get { return _PropertyType; }
            set
            {
                SetPropertyValue("PropertyType", ref _PropertyType, value);
                if (!IsLoading)
                {
                    if (PropertyType != null)
                    {
                        if (string.IsNullOrEmpty(Name))
                        {
                            Name = PropertyType.Caption;
                        }
                    }
                }
            }
        }


        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [XafDisplayName("��ʾ����")]
        public string DisplayName
        {
            get
            {
                if (BusinessObject != null)
                    return this.BusinessObject.FullName + "." + this.Name;
                return this.Name;
            }
        }
        #endregion

        #region ��������:һ�Զ���Զ�ʱ,�������ԵĶ�Ӧ��ϵ.
        protected virtual bool RelationPropertyNotNull
        {
            get
            {
                return false;
            }
        }

        private PropertyBase _RelationProperty;
        [XafDisplayName("��������"), DataSourceProperty("RelationPropertyDataSources")]
        [RuleRequiredField(TargetCriteria = "RelationPropertyNotNull"), LookupEditorMode(LookupEditorMode.AllItems)]
        [ToolTip("һ�Զ���Զ�ʱ,�������ԵĶ�Ӧ��ϵ.")]
        public PropertyBase RelationProperty
        {
            get { return _RelationProperty; }
            set
            {
                SetPropertyValue("RelationProperty", ref _RelationProperty, value);
                if (!IsLoading && !IsSaving && value != null)
                {
                    if (value.RelationProperty != this)
                        value.RelationProperty = this;
                }
            }
        }

        protected virtual List<PropertyBase> RelationPropertyDataSources
        {
            get
            {
                return PropertyType?.Properties.Where(x => x.PropertyType == BusinessObject).OfType<PropertyBase>().ToList();
            }
        }
        #endregion

        
        #region �ɼ���
        private bool? _Browsable;
        [XafDisplayName("�ɼ�")]
        [ToolTip("�������κ�λ���Ƿ�ɼ�")]
        public bool? Browsable
        {
            get { return _Browsable; }
            set { SetPropertyValue("Browsable", ref _Browsable, value); }
        }
        #endregion

        #region �ڿͻ��˽������ü���
        //private string _DataSourceProperty;
        //[XafDisplayName("������Դ����")]
        //public string DataSourceProperty
        //{
        //    get { return _DataSourceProperty; }
        //    set { SetPropertyValue("DataSourceProperty", ref _DataSourceProperty, value); }
        //}

        //[XafDisplayName("ʧЧ����")]
        //[ToolTip("��������Դ������ָ��������Ϊ��ʱ,��δ���")]
        //public DataSourcePropertyIsNullMode DataSourcePropertyIsNullMode
        //{
        //    get { return GetPropertyValue<DataSourcePropertyIsNullMode>(nameof(DataSourcePropertyIsNullMode)); }
        //    set { SetPropertyValue(nameof(DataSourcePropertyIsNullMode), value); }
        //}

        //[XafDisplayName("��������")]
        //[ToolTip("��������Դ������ָ��������Ϊ��ʱ,�ٴ�ʹ������������в�ѯ,ǰ����:ʧЧ������ѡ����CustomCriteria(�Զ�������)")]
        //public string DataSourceIsNullCriteria
        //{
        //    get { return GetPropertyValue<string>(nameof(DataSourceIsNullCriteria)); }
        //    set { SetPropertyValue(nameof(DataSourceIsNullCriteria), value); }
        //}

        //private bool? _VisibleInDetailView;
        //[XafDisplayName("��ϸ��ͼ�ɼ�")]
        //public bool? VisibleInDetailView
        //{
        //    get { return _VisibleInDetailView; }
        //    set { SetPropertyValue("VisibleInDetailView", ref _VisibleInDetailView, value); }
        //}

        //private bool? _VisibleInListView;
        //[XafDisplayName("�б���ͼ�ɼ�")]
        //public bool? VisibleInListView
        //{
        //    get { return _VisibleInListView; }
        //    set { SetPropertyValue("VisibleInListView", ref _VisibleInListView, value); }
        //}

        //private bool? _VisibleInLookupView;
        //[XafDisplayName("������ͼ�ɼ�")]
        //public bool? VisibleInLookupView
        //{
        //    get { return _VisibleInLookupView; }
        //    set { SetPropertyValue("VisibleInLookupView", ref _VisibleInLookupView, value); }
        //}

        #region ����༭
        //private bool? _AllowEdit;
        //[XafDisplayName("����༭")]
        //public bool? AllowEdit
        //{
        //    get { return _AllowEdit; }
        //    set { SetPropertyValue("AllowEdit", ref _AllowEdit, value); }
        //}
        #endregion

        #region �����ط�
        //private bool? _ImmediatePostData;
        //[XafDisplayName("�����ط�")]
        //[ToolTip("������ֵ�����仯��,����֪ͨϵͳ,ϵͳ���Լ�ʱ���������ز���,ͨ�����ڹ�ʽ����������,web�н�Ϊ����.")]
        //public bool? ImmediatePostData
        //{
        //    get { return _ImmediatePostData; }
        //    set { SetPropertyValue("ImmediatePostData", ref _ImmediatePostData, value); }
        //}
        #endregion

        #region ��ʾ��༭��ʽ
        //private string _DisplayFormat;
        //[XafDisplayName("��ʾ��ʽ")]
        //public string DisplayFormat
        //{
        //    get { return _DisplayFormat; }
        //    set { SetPropertyValue("DisplayFormat", ref _DisplayFormat, value); }
        //}

        //private string _EditMask;
        //[XafDisplayName("�༭��ʽ")]
        //public string EditMask
        //{
        //    get { return _EditMask; }
        //    set { SetPropertyValue("EditMask", ref _EditMask, value); }
        //}
        #endregion

        #region ֵ��Χ
        //private RuleRange _Range;
        //[XafDisplayName("��Χ")]
        //[VisibleInListView(false)]
        //[ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        //public RuleRange Range
        //{
        //    get { return _Range; }
        //    set { SetPropertyValue("Range", ref _Range, value); }
        //}
        #endregion

        #region ��֤
        #region ����

        //private bool? _RuleRequiredField;
        //[XafDisplayName("����")]
        //public bool? RuleRequiredField
        //{
        //    get { return _RuleRequiredField; }
        //    set { SetPropertyValue("RuleRequiredField", ref _RuleRequiredField, value); }
        //}
        //#endregion

        //#region Ψһ
        //private bool? _UniqueValue;
        //[XafDisplayName("Ψһ")]
        //public bool? UniqueValue
        //{
        //    get { return _UniqueValue; }
        //    set { SetPropertyValue("UniqueValue", ref _UniqueValue, value); }
        //}
        #endregion
        #endregion 
        #endregion
        public PropertyBase(Session s) : base(s)
        {
        }


    }
}