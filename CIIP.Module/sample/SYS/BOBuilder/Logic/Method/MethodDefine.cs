using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Validation;
using ���û���;

namespace CIIP.Module.BusinessObjects.SYS.Logic
{
    [XafDisplayName("��������")]
    [ImageName("PG_Method")]
    [DefaultListViewOptions(MasterDetailMode.ListViewAndDetailView)]
    public class MethodDefine : NameObject
    {
        public MethodDefine(Session s) : base(s)
        {

        }

        private BusinessObject _BusinessObject;

        [Association]
        [Browsable(false)]
        public BusinessObject BusinessObject
        {
            get { return _BusinessObject; }
            set { SetPropertyValue("BusinessObject", ref _BusinessObject, value); }
        }
        
        private BusinessObjectBase _ReturnValue;
        [XafDisplayName("��������")]
        public virtual BusinessObjectBase ReturnValue
        {
            get { return _ReturnValue; }
            set { SetPropertyValue("ReturnValue", ref _ReturnValue, value); }
        }
        
        private string _Code;
        [EditorAlias("CodeEditor")]
        public string Code
        {
            get { return _Code; }
            set { SetPropertyValue("Code", ref _Code, value); }
        }

        private AccessorModifier _AccessorModifier;
        [XafDisplayName("��������")]
        public AccessorModifier AccessorModifier
        {
            get { return _AccessorModifier; }
            set { SetPropertyValue("AccessorModifier", ref _AccessorModifier, value); }
        }


        private MethodModifier _methodModifier;
        [XafDisplayName("��������")]
        public MethodModifier MethodModifier
        {
            get { return _methodModifier; }
            set { SetPropertyValue("MethodModifier", ref _methodModifier, value); }
        }

        private string _CodeName;
        [XafDisplayName("��������")]
        [RuleRequiredField]
        public string CodeName
        {
            get { return _CodeName; }
            set { SetPropertyValue("CodeName", ref _CodeName, value); }
        }

        private string _����˵��;
        [Size(-1)]
        public string ����˵��
        {
            get { return _����˵��; }
            set { SetPropertyValue("����˵��", ref _����˵��, value); }
        }

        public string DefaultLocation
        {
            get { return "//MethodDefine:"+this.Oid; }
        }

        public string ReplaceNewCode(string allcode, string newCode)
        {
            var b = allcode.IndexOf(DefaultLocation) + DefaultLocation.Length + 1;
            var e = allcode.IndexOf(DefaultLocation + "End") - 1;
            allcode = allcode.Remove(b, e - b);
            return allcode.Insert(b, newCode);
        }
        
        public virtual string MethodDefineCode
        {
            get
            {
                return string.Format(@"
{0} {1} {6} {2}({3}){{
{4}

{5}

{4}End

}}
",
                    AccessorModifier.ToString().ToLower(),
                    MethodModifier == MethodModifier.None ? "" : MethodModifier.ToString().ToLower(),
                    this.CodeName,
                    string.Join(",", Parameters.Select(x => $"{ (x.ParameterType.IndexOf(".") > -1 ? "global::" : "") + x.ParameterType} {x.ParameterName}")),
                    this.DefaultLocation,
                    this.Code,
                    this.ReturnValue == null ? "void" : "global::" + ReturnValue.FullName
                    );
            }
        }

        [Association, DevExpress.Xpo.Aggregated]
        public XPCollection<ParameterDefine> Parameters
        {
            get { return GetCollection<ParameterDefine>("Parameters"); }
        }
    }

    public enum MethodModifier
    {
        None,
        Override,
        Virtual,
        Abstract
    }

    public enum AccessorModifier
    {
        Public,
        Protected,
        Private,
        Internal,

    }

    //public class MethodDefine_ListView : MethodViewBase<MethodDefine>
    //{

    //}

    //public class ObjectAfterConstruction_ListView : MethodViewBase<ObjectAfterConstruction>
    //{

    //}

    //public class ObjectDeletingEvent_ListView : MethodViewBase<ObjectDeletingEvent>
    //{

    //}

    //public class ObjectSavingEvent_ListView : MethodViewBase<ObjectSavingEvent>
    //{

    //}

    //public class ObjectSavedEvent_ListView : MethodViewBase<ObjectSavedEvent>
    //{

    //}

    //public class ObjectPropertyChangedEvent_ListView : MethodViewBase<ObjectPropertyChangedEvent>
    //{

    //}

}