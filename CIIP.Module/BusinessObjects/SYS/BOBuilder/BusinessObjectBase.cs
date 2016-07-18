using System;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using ���û���;

namespace CIIP.Module.BusinessObjects.SYS
{
    [XafDisplayName("����")]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    public class BusinessObjectBase : NameObject,ICategorizedItem
    {
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

        private NameSpace _Category;
        [XafDisplayName("����")]
        [RuleRequiredField]
        public NameSpace Category
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
        [XafDisplayName("˵��")]
        public string Description
        {
            get { return _Description; }
            set { SetPropertyValue("Description", ref _Description, value); }
        }


        ITreeNode ICategorizedItem.Category
        {
            get { return Category; }
            set { Category = (NameSpace) value; }
        }

        public BusinessObjectBase(Session s) : base(s)
        {
        }
    }
}