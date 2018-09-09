using System.Collections.Generic;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using CIIP.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using System.Linq;
using DevExpress.ExpressApp.ConditionalAppearance;
using System;

namespace CIIP.Designer
{
    /// <summary>
    /// 
    /// </summary>
    [XafDefaultProperty("DisplayName")]
    [Appearance("PropertyBase.RelationIsEnable", TargetItems = "RelationProperty", Enabled = false, Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Method = "RelationIsEnable")]
    [Appearance("PropertyBase.RelationPropertyStateByAutoCreate", TargetItems = "RelationProperty", Criteria = "AutoCreateRelationProperty", Enabled = false)]
    //�Զ��ʱ,�Զ������Ǳ����.
    //һ�Զ�ʱ,��ѡ�ֶ�����,Ĭ�����Զ�����.
    public abstract class PropertyBase : NameObject
    {
        private string _Expression;

        [XafDisplayName("���㹫ʽ")]
        [ToolTip("�����˹�ʽ�󣬴����Խ�Ϊֻ����ʹ�ù�ʽ���м���")]
        public string Expression
        {
            get { return _Expression; }
            set { SetPropertyValue("Expression", ref _Expression, value); }
        }

        public static bool RelationIsEnable(PropertyBase obj)
        {
            return obj != null && obj.PropertyType is SimpleType;
        }

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
        [EditorAlias(Editors.PropertyTypeTokenEditor),DataSourceProperty(nameof(PropertyTypes))]
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
        
        private BusinessObject[] types;

        protected IEnumerable<BusinessObjectBase> PropertyTypes
        {
            get
            {
                if (types == null)
                {
                    types = Session.Query<BusinessObject>().Where(x => x.IsPersistent).ToArray();
                }
                return types;
            }
        }

        [ToolTip("������ʾ�ڽ����ϵı�������.")]
        [XafDisplayName("����")]
        public string Caption
        {
            get { return GetPropertyValue<string>(nameof(Caption)); }
            set { SetPropertyValue(nameof(Caption), value); }
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
        [RuleRequiredField(TargetCriteria = "RelationPropertyNotNull && !AutoCreateRelationProperty"), LookupEditorMode(LookupEditorMode.AllItems)]
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

        [CaptionsForBoolValues("�Զ�", "�ֶ�")]
        [XafDisplayName("������������")]
        [ImmediatePostData]
        public bool AutoCreateRelationProperty
        {
            get { return GetPropertyValue<bool>(nameof(AutoCreateRelationProperty)); }
            set { SetPropertyValue(nameof(AutoCreateRelationProperty), value); }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (IsLoading || IsSaving)
                return;

            if (propertyName == nameof(AutoCreateRelationProperty))
            {
                if (AutoCreateRelationProperty)
                {
                    RelationProperty = null;
                }
            }
            if (propertyName == nameof(PropertyType) && AutoCreateRelationProperty)
            {
                if (PropertyType != null)
                {
                    if (string.IsNullOrEmpty(Name))
                    {
                        Name = PropertyType.Name;
                        Caption = PropertyType.Caption;
                    }

                    //����һ������,�޸�Ϊ�Զ�����һ��.
                    //if (RelationProperty == null)
                    //{
                    //    try
                    //    {
                    //        RelationProperty = PropertyType.Properties.SingleOrDefault(x => x.PropertyType.Oid == this.BusinessObject.Oid);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        throw ex;
                    //    }
                    //}

                    //if (RelationProperty == null)
                    //{
                    //    RelationProperty = PropertyType.Properties.SingleOrDefault(x => x.PropertyType.Oid == this.BusinessObject.Oid);
                    //}
                }
                else
                {
                    Name = "";
                }

            }

        }

        [ModelDefault("AllowEdit","False")]
        public bool IsAutoCreated
        {
            get { return GetPropertyValue<bool>(nameof(IsAutoCreated)); }
            set { SetPropertyValue(nameof(IsAutoCreated), value); }
        }





        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Browsable = true;
            AutoCreateRelationProperty = true;
        }

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
        private bool? _AllowEdit;
        [XafDisplayName("����༭")]
        public bool? AllowEdit
        {
            get { return _AllowEdit; }
            set { SetPropertyValue("AllowEdit", ref _AllowEdit, value); }
        }
        #endregion

        #region �����ط�
        private bool? _ImmediatePostData;
        [XafDisplayName("�����ط�")]
        [ToolTip("������ֵ�����仯��,����֪ͨϵͳ,ϵͳ���Լ�ʱ���������ز���,ͨ�����ڹ�ʽ����������,web�н�Ϊ����.")]
        public bool? ImmediatePostData
        {
            get { return _ImmediatePostData; }
            set { SetPropertyValue("ImmediatePostData", ref _ImmediatePostData, value); }
        }
        #endregion

        #region ��ʾ��༭��ʽ
        private string _DisplayFormat;
        [XafDisplayName("��ʾ��ʽ")]
        public string DisplayFormat
        {
            get { return _DisplayFormat; }
            set { SetPropertyValue("DisplayFormat", ref _DisplayFormat, value); }
        }

        private string _EditMask;
        [XafDisplayName("�༭��ʽ")]
        public string EditMask
        {
            get { return _EditMask; }
            set { SetPropertyValue("EditMask", ref _EditMask, value); }
        }
        #endregion

        #region ֵ��Χ
        private RuleRange _Range;
        [XafDisplayName("��Χ")]
        [VisibleInListView(false)]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public RuleRange Range
        {
            get { return _Range; }
            set { SetPropertyValue("Range", ref _Range, value); }
        }
        #endregion

        #region ��֤
        #region ����

        private bool? _RuleRequiredField;
        [XafDisplayName("����")]
        public bool? RuleRequiredField
        {
            get { return _RuleRequiredField; }
            set { SetPropertyValue("RuleRequiredField", ref _RuleRequiredField, value); }
        }
        #endregion

        #region Ψһ
        private bool? _UniqueValue;
        [XafDisplayName("Ψһ")]
        public bool? UniqueValue
        {
            get { return _UniqueValue; }
            set { SetPropertyValue("UniqueValue", ref _UniqueValue, value); }
        }
        #endregion
        #endregion 
        #endregion
        public PropertyBase(Session s) : base(s)
        {
        }


    }
}