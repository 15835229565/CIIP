using DevExpress.Xpo;

namespace CRM
{
    public class ����������ͨ��¼ : ��ͨ��¼Base
    {
        public ����������ͨ��¼(Session s) : base(s)
        {
        }

        private �������� _��������;
        [Association]
        public �������� ��������
        {
            get { return _��������; }
            set { SetPropertyValue("��������", ref _��������, value); }
        }
    }
}