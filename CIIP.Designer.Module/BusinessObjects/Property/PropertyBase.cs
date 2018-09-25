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
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Editors;

namespace CIIP.Designer
{

    /// <summary>
    /// 
    /// </summary>
    [XafDefaultProperty("DisplayName")]
    [Appearance("HiddenAssicationInfo",TargetItems =nameof(AssocicationInfo), Enabled =false,Visibility = ViewItemVisibility.Hide,Criteria = "!IsAssocication")]
    [Appearance("HiddenIsAssocication", TargetItems = nameof(IsAssocication), Enabled = false, Visibility = ViewItemVisibility.Hide, Criteria = "PropertyType is null or !PropertyType.CanCreateAssocication")]
    //�Զ��ʱ,�Զ������Ǳ����.
    //һ�Զ�ʱ,��ѡ�ֶ�����,Ĭ�����Զ�����.
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

        [XafDisplayName("����"), RuleRequiredField]
        [ImmediatePostData]
        [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
        //[EditorAlias(Editors.PropertyTypeTokenEditor)]
        [DataSourceProperty(nameof(PropertyTypes))]
        public virtual BusinessObjectBase PropertyType
        {
            get { return GetPropertyValue<BusinessObjectBase>(nameof(PropertyType)); }
            set
            {
                SetPropertyValue("PropertyType", value);
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

        protected BusinessObjectBase[] propertyTypes;

        protected virtual IEnumerable<BusinessObjectBase> PropertyTypes
        {
            get
            {
                if (propertyTypes == null)
                {
                    propertyTypes = Session.Query<BusinessObjectBase>().ToArray();
                }
                return propertyTypes;
            }
        }

        [ToolTip("������ʾ�ڽ����ϵı�������.")]
        [XafDisplayName("����")]
        [ImmediatePostData]
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
        
        #region �ɼ���
        [XafDisplayName("�ɼ�")]
        [ToolTip("�������κ�λ���Ƿ�ɼ�")]
        public bool Browsable
        {
            get { return GetPropertyValue<bool>(nameof(Browsable)); }
            set { SetPropertyValue(nameof(Browsable), value); }
        }
        #endregion

        public void CalcNameCaption()
        {
            if (PropertyType == null) return;
            if (string.IsNullOrEmpty(Name))
            {
                Name = PropertyType.Name;
                Caption = PropertyType.Caption;
            }
        }


        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Browsable = true;
            AllowEdit = true;
        }

        //[ModelDefault("AllowEdit", "False")]
        [XafDisplayName("����")]
        [EditorAlias("OPE"),Association,VisibleInDetailView(true)]
        public AssocicationInfo AssocicationInfo
        {
            get { return GetPropertyValue<AssocicationInfo>(nameof(AssocicationInfo)); }
            set { SetPropertyValue(nameof(AssocicationInfo), value); }
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
        [XafDisplayName("����༭")]
        public bool AllowEdit
        {
            get { return GetPropertyValue<bool>(nameof(AllowEdit)); ; }
            set { SetPropertyValue(nameof(AllowEdit),value); }
        }
        #endregion


        #endregion

        [XafDisplayName("��ϵ"),ImmediatePostData]
        public bool IsAssocication
        {
            get { return GetPropertyValue<bool>(nameof(IsAssocication)); }
            set { SetPropertyValue(nameof(IsAssocication), value); }
        }

        [XafDisplayName("˵��")]
        [Size(-1)]
        [ModelDefault("RowCount","2")]
        public string Memo
        {
            get { return GetPropertyValue<string>(nameof(Memo)); }
            set { SetPropertyValue(nameof(Memo), value); }
        }

        public PropertyBase(Session s) : base(s)
        {
        }
    }
}