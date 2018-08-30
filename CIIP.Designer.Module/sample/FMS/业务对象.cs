using System;
using DevExpress.ExpressApp.Utils;
using DevExpress.Xpo;

namespace CIIP.Module.BusinessObjects
{
    public class ҵ����� : XPLiteObject
    {
        public ҵ�����(Session s) : base(s)
        {

        }

        private string _Oid;
        [Key(false)]
        public string Oid
        {
            get { return _Oid; }
            set { SetPropertyValue("Oid", ref _Oid, value); }
        }


        private Type _Type;
        [ValueConverter(typeof(TypeToStringConverter))]
        public Type Type
        {
            get { return _Type; }
            set
            {
                SetPropertyValue("Type", ref _Type, value);
                if (!IsLoading)
                {
                    Oid = value?.FullName;
                }
            }
        }

        [Association()]
        public XPCollection<�������> �������
        {
            get { return GetCollection<�������>("�������"); }
        }
    }
}