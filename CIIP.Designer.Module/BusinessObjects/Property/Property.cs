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

        [XafDisplayName("���㹫ʽ")]
        [ToolTip("�����˹�ʽ�󣬴����Խ�Ϊֻ����ʹ�ù�ʽ���м���")]
        public string Expression
        {
            get { return GetPropertyValue<string>(nameof(Expression)); }
            set { SetPropertyValue(nameof(Expression), value); }
        }

        public static bool SizeIsVisible(Property property)
        {
            return property?.PropertyType?.FullName != typeof(string).FullName;
        }
        #region �����ط�
        [XafDisplayName("�����ط�")]
        [ToolTip("������ֵ�����仯��,����֪ͨϵͳ,ϵͳ���Լ�ʱ���������ز���,ͨ�����ڹ�ʽ����������,web�н�Ϊ����.")]
        public bool ImmediatePostData
        {
            get { return GetPropertyValue<bool>(nameof(ImmediatePostData)); }
            set { SetPropertyValue(nameof(ImmediatePostData), value); }
        }
        #endregion
        
        #region ��ʾ��༭��ʽ
        [XafDisplayName("��ʾ��ʽ")]
        public string DisplayFormat
        {
            get { return GetPropertyValue<string>(nameof(DisplayFormat)); }
            set { SetPropertyValue(nameof(DisplayFormat), value); }
        }

        [XafDisplayName("�༭��ʽ")]
        public string EditMask
        {
            get { return GetPropertyValue<string>(nameof(EditMask)); }
            set { SetPropertyValue(nameof(EditMask), value); }
        }
        #endregion

        #region ֵ��Χ
        [XafDisplayName("��Χ")]
        [VisibleInListView(false)]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public RuleRange Range
        {
            get { return GetPropertyValue<RuleRange>(nameof(Range)); }
            set { SetPropertyValue(nameof(Range), value); }
        }
        #endregion

        #region ��֤
        #region ����

        [XafDisplayName("����")]
        public bool RuleRequiredField
        {
            get { return GetPropertyValue<bool>(nameof(RuleRequiredField)); }
            set { SetPropertyValue(nameof(RuleRequiredField), value); }
        }
        #endregion

        #region Ψһ
        [XafDisplayName("Ψһ")]
        public bool UniqueValue
        {
            get { return GetPropertyValue<bool>(nameof(UniqueValue)); }
            set { SetPropertyValue(nameof(UniqueValue), value); }
        }
        #endregion
        #endregion 

        [XafDisplayName("����")]
        public int Size
        {
            get { return GetPropertyValue<int>(nameof(Size)); }
            set { SetPropertyValue(nameof(Size), value); }
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