using CIIP.Module.BusinessObjects;
using DevExpress.Xpo;

namespace CRM
{
    public class ��ͨ��¼ : ��ͨ��¼Base
    {
        public ��ͨ��¼(Session s) : base(s)
        {
        }

        private ������λ _������λ;
        [Association]
        public ������λ ������λ
        {
            get { return _������λ; }
            set { SetPropertyValue("������λ", ref _������λ, value); }
        }
    }
}