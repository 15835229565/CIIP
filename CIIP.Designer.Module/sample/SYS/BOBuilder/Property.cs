using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CIIP.Module.BusinessObjects.SYS
{
    public class Property : PropertyBase
    {
        private BusinessObject _Owner;

        [Association]
        public BusinessObject Owner
        {
            get { return _Owner; }
            set { SetPropertyValue("Owner", ref _Owner, value); }
        }

        private string _Expression;

        [XafDisplayName("���㹫ʽ")]
        [ToolTip("�����˹�ʽ�󣬴����Խ�Ϊֻ����ʹ�ù�ʽ���м���")]
        public string Expression
        {
            get { return _Expression; }
            set { SetPropertyValue("Expression", ref _Expression, value); }
        }


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
                        if (string.IsNullOrEmpty(����))
                        {
                            ���� = PropertyType.Caption;
                        }
                    }
                    else
                    {
                        
                        ���� = "";
                    }
                }
            }
        }

        private int _Size;
        public int Size
        {
            get { return _Size; }
            set { SetPropertyValue("Size", ref _Size, value); }
        }

        protected override BusinessObject OwnerBusinessObject
        {
            get
            {
                return this.Owner;
            }
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