using DevExpress.Xpo;

namespace CRM
{
    public class �ͷ���ͨ��¼ : ��ͨ��¼Base
    {
        public �ͷ���ͨ��¼(Session s) : base(s)
        {

        }

        private �ͷ��绰���� _�ͷ�����;
        [Association]
        public �ͷ��绰���� �ͷ�����
        {
            get { return _�ͷ�����; }
            set { SetPropertyValue("�ͷ�����", ref _�ͷ�����, value); }
        }

    }
}