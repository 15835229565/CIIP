using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.DC;
using DevExpress.Xpo;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace CIIP.Designer
{
    [XafDisplayName("�ӱ�")]
    [Appearance("ManyToManyHiddenAggregated",Criteria = "ManyToMany", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, TargetItems = "Aggregated")]
    [Appearance("PropertyBase.AutoCreateRelationIsEnable", TargetItems = "AutoCreateRelationProperty", Criteria = "ManyToMany", Enabled = false)]
    public class CollectionProperty : PropertyBase
    {
        public CollectionProperty(Session s) : base(s)
        {
        }
        
        protected override bool RelationPropertyNotNull
        {
            get
            {
                return true;
            }
        }
        protected override void OnSaving()
        {
            base.OnSaving();
            CreateRelationProperty();
        }
        protected void CreateRelationProperty()
        {
            if (!IsAutoCreated && AutoCreateRelationProperty && Session.IsNewObject(this))
            {
                if (ManyToMany)
                {
                    //��ǰ��xpcollection<ѧ��> ѧ��s {get;} ����
                    //�Զ������������� xpcollection<��ʦ> ��ʦs {get;} ����
                }
                else
                {
                    //��ǰ��xpcollection<order> orders {get;} ����
                    //�Զ������������� customer customer {get;} ����
                    var property = new Property(Session);
                    property.BusinessObject = this.PropertyType;
                    property.PropertyType = this.BusinessObject;
                    property.AutoCreateRelationProperty = false;
                    property.Name = BusinessObject.Name;
                    property.Caption = BusinessObject.Caption;
                    property.IsAutoCreated = true;
                    property.RelationProperty = this;
                    RelationProperty = property;
                }
            }
        }
        
        private bool _Aggregated;
        [XafDisplayName("�ۺ�")]
        public bool Aggregated
        {
            get { return _Aggregated; }
            set { SetPropertyValue("Aggregated", ref _Aggregated, value); }
        }

        [CaptionsForBoolValues("�Զ��", "һ�Զ�")]
        [XafDisplayName("��ϵ����")]
        [ImmediatePostData]
        public bool ManyToMany
        {
            get { return GetPropertyValue<bool>(nameof(ManyToMany)); }
            set { SetPropertyValue(nameof(ManyToMany), value); }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!IsLoading)
            {
                if (propertyName == nameof(ManyToMany))
                {
                    AutoCreateRelationProperty = true;
                }
            }
        }



        protected override List<PropertyBase> RelationPropertyDataSources
        {
            get
            {
                if (PropertyType == null)
                    return null;
                return PropertyType.Properties.Where(x => x.PropertyType == this.BusinessObject).OfType<PropertyBase>().ToList();
            }
        }
    }

}