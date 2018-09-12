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
    [Appearance("ManyToManyHiddenAggregated", AppearanceItemType = "LayoutItem", Criteria = "AssocicationInfo.ManyToMany", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, TargetItems = "Aggregated")]
    [Appearance("LVisible", AppearanceItemType = "LayoutItem" , Criteria = "Oid == AssocicationInfo.LeftProperty.Oid", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, TargetItems = "LT,LP")]
    [Appearance("RVisible", AppearanceItemType = "LayoutItem", Criteria = "Oid != AssocicationInfo.LeftProperty.Oid", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, TargetItems = "RT,RP")]
    public class CollectionProperty : PropertyBase
    {
        public CollectionProperty(Session s) : base(s)
        {
        }

        public override BusinessObjectBase PropertyType
        {
            get
            {
                if (AssocicationInfo?.LeftProperty?.Oid != this.Oid)
                {
                    return AssocicationInfo?.LeftTable;
                }
                return AssocicationInfo?.RightTable;
            }
            set => base.PropertyType = value;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            AssocicationInfo = new AssocicationInfo(Session);
            AssocicationInfo.RightProperty = this;
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            CreateRelationProperty();
        }
        protected void CreateRelationProperty()
        {
            //if (ManyToMany)
            //{
            //    //��ǰ��xpcollection<ѧ��> ѧ��s {get;} ����
            //    //�Զ������������� xpcollection<��ʦ> ��ʦs {get;} ����
            //    var property = new CollectionProperty(Session);
            //    property.BusinessObject = this.PropertyType;
            //    property.PropertyType = this.BusinessObject;
            //    property.ManyToMany = true;
            //    property.Name = BusinessObject.Name;
            //    property.Caption = BusinessObject.Caption;
            //    property.RelationProperty = this;
            //    RelationProperty = property;
            //}
            //else
            //{
            //    //��ǰ��xpcollection<order> orders {get;} ����
            //    //�Զ������������� customer customer {get;} ����
            //    var property = new Property(Session);
            //    property.BusinessObject = this.PropertyType;
            //    property.PropertyType = this.BusinessObject;
            //    property.Name = BusinessObject.Name;
            //    property.Caption = BusinessObject.Caption;
            //    property.RelationProperty = this;
            //    RelationProperty = property;
            //}
        }

        private bool _Aggregated;
        [XafDisplayName("�ۺ�")]
        public bool Aggregated
        {
            get { return _Aggregated; }
            set { SetPropertyValue("Aggregated", ref _Aggregated, value); }
        }
        protected override IEnumerable<BusinessObjectBase> PropertyTypes
        {
            get
            {
                if (propertyTypes == null)
                {
                    propertyTypes = Session.Query<BusinessObject>().Where(x => x.IsPersistent).ToArray();
                }
                return propertyTypes;
            }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (IsLoading || IsSaving) return;

            //**********************************************************
            //Ӧ�ò��ܷŵ�������ȥ,������ܵ��´�����޸��ұ�,��Ҫ��֤
            //**********************************************************
            if (propertyName == nameof(this.BusinessObject))
            {
                AssocicationInfo.RightTable = this.BusinessObject;
            }
        }


    }

}