using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CIIP
{
    public class Editors
    {
        public const string PropertyTypeTokenEditor = "PropertyTypeTokenEditor";
    }
}

namespace CIIP.Designer
{

    [XafDisplayName("����")]
    [Appearance("SizeIsVisible",TargetItems ="Size",Method = "SizeIsVisible", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    public class Property : PropertyBase
    {
        private string _Expression;

        [XafDisplayName("���㹫ʽ")]
        [ToolTip("�����˹�ʽ�󣬴����Խ�Ϊֻ����ʹ�ù�ʽ���м���")]
        public string Expression
        {
            get { return _Expression; }
            set { SetPropertyValue("Expression", ref _Expression, value); }
        }

        public static bool SizeIsVisible(Property property)
        {
            return property?.PropertyType?.FullName != typeof(string).FullName;
        }
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

        private int _Size;
        [XafDisplayName("����")]
        public int Size
        {
            get { return _Size; }
            set { SetPropertyValue("Size", ref _Size, value); }
        }
        
        

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Size = 100;
        }

        public Property(Session s) : base(s)
        {

        }
    }
}