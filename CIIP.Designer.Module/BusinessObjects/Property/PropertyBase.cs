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
    //[Appearance("PropertyBase.RelationPropertyStateByAutoCreate", TargetItems = "RelationProperty", Criteria = "AutoCreateRelationProperty", Enabled = false)]
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
        private BusinessObjectBase _PropertyType;

        [XafDisplayName("����"), RuleRequiredField]
        [ImmediatePostData]
        [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
        //[EditorAlias(Editors.PropertyTypeTokenEditor)]
        [DataSourceProperty(nameof(PropertyTypes))]
        public virtual BusinessObjectBase PropertyType
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

        #region ��������:һ�Զ���Զ�ʱ,�������ԵĶ�Ӧ��ϵ.
        //protected virtual bool RelationPropertyNotNull
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}

        //private PropertyBase _RelationProperty;
        //[XafDisplayName("��������"), DataSourceProperty("RelationPropertyDataSources")]
        //[RuleRequiredField(TargetCriteria = "RelationPropertyNotNull"), LookupEditorMode(LookupEditorMode.AllItems)]
        //[ToolTip("һ�Զ���Զ�ʱ,�������ԵĶ�Ӧ��ϵ.")]
        //public PropertyBase RelationProperty
        //{
        //    get { return _RelationProperty; }
        //    set
        //    {
        //        SetPropertyValue("RelationProperty", ref _RelationProperty, value);
        //        if (!IsLoading && !IsSaving && value != null)
        //        {
        //            if (value.RelationProperty != this)
        //                value.RelationProperty = this;
        //        }
        //    }
        //}

        //protected virtual List<PropertyBase> RelationPropertyDataSources
        //{
        //    get
        //    {
        //        return PropertyType?.Properties.Where(x => x.PropertyType == BusinessObject).OfType<PropertyBase>().ToList();
        //    }
        //}
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

        public void CalcNameCaption()
        {
            if (PropertyType == null) return;
            if (string.IsNullOrEmpty(Name))
            {
                Name = PropertyType.Name;
                Caption = PropertyType.Caption;
            }
        }
        //protected override void OnChanged(string propertyName, object oldValue, object newValue)
        //{
        //    base.OnChanged(propertyName, oldValue, newValue);
        //    if (IsLoading) return;
        //    if(propertyName == nameof(Name))
        //    {
        //        AssocicationInfo?.CalcName();
        //        CalcNameCaption();
        //    }
        //}
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Browsable = true;
        }

        //[ModelDefault("AllowEdit", "False")]
        [XafDisplayName("����")]
        [EditorAlias(EditorAliases.ObjectPropertyEditor)]
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
        private bool? _AllowEdit;
        [XafDisplayName("����༭")]
        public bool? AllowEdit
        {
            get { return _AllowEdit; }
            set { SetPropertyValue("AllowEdit", ref _AllowEdit, value); }
        }
        #endregion


        #endregion

        public PropertyBase(Session s) : base(s)
        {
        }


    }

    public class PropertyBaseViewController : ObjectViewController<ObjectView, PropertyBase>
    {
        public PropertyBaseViewController()
        {
            var action = new SimpleAction(this, "CreateRelationProperty", "CreateRelationProperty");
            action.Caption = "����";
            action.ImageName = "Action_New";
            action.Execute += Action_Execute;
        }

        private void Action_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //var os = this.ObjectSpace.CreateNestedObjectSpace();
            //var obj = CreateRelationProperty(os.GetObject(this.ViewCurrentObject), os);

            //os.Committed += (s, evt) =>
            //{
            //    this.ViewCurrentObject.AssocicationInfo.LeftProperty = this.ObjectSpace.GetObject(obj);

            //};

            //var view = Application.CreateDetailView(os, obj, true);
            //e.ShowViewParameters.CreatedView = view;
            //e.ShowViewParameters.Context = TemplateContext.PopupWindow;
            //e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
            //e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            //var dc = new DialogController();

            //dc.Accepting += (s, evt) =>
            //{
            //    //os.CommitChanges();
            //};

            //e.ShowViewParameters.Controllers.Add(dc);

        }

        //public PropertyBase CreateRelationProperty(PropertyBase currentProperty, IObjectSpace os)
        //{
        //    PropertyBase property;
        //    if (currentProperty.AssocicationInfo.ManyToMany)
        //    {
        //        //��ǰ��xpcollection<ѧ��> ѧ��s {get;} ����
        //        //�Զ������������� xpcollection<��ʦ> ��ʦs {get;} ����
        //        property = new CollectionProperty((os as XPObjectSpace).Session, currentProperty.AssocicationInfo);// os.CreateObject<CollectionProperty>();
        //    }
        //    else
        //    {
        //        //��ǰ��xpcollection<order> orders {get;} ����
        //        //�Զ������������� customer customer {get;} ����
        //        property = os.CreateObject<Property>();
        //    }
        //    property.BusinessObject = currentProperty.PropertyType;
        //    property.PropertyType = currentProperty.BusinessObject;
        //    property.Name = currentProperty.BusinessObject.Name;
        //    property.Caption = currentProperty.BusinessObject.Caption;
        //    return property;
        //}
    }
}