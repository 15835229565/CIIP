using DevExpress.Xpo;
using CIIP;
using CIIP.Module.BusinessObjects;
using ���û���;

namespace CRM
{
    public class �������� : SimpleObject
    {
        public ��������(Session s) : base(s)
        {
        }

        private string _����;
        public string ����
        {
            get { return _����; }
            set { SetPropertyValue("����", ref _����, value); }
        }

        private string _��˾����;
        public string ��˾����
        {
            get { return _��˾����; }
            set { SetPropertyValue("��˾����", ref _��˾����, value); }
        }

        private ������λ _�ͻ�;
        public ������λ �ͻ�
        {
            get { return _�ͻ�; }
            set { SetPropertyValue("�ͻ�", ref _�ͻ�, value); }
        }

        private string _��ַ;
        public string ��ַ
        {
            get { return _��ַ; }
            set { SetPropertyValue("��ַ", ref _��ַ, value); }
        }

        private string _��ϵ������;
        public string ��ϵ������
        {
            get { return _��ϵ������; }
            set { SetPropertyValue("��ϵ������", ref _��ϵ������, value); }
        }

        private string _�����ʼ�;
        public string �����ʼ�
        {
            get { return _�����ʼ�; }
            set { SetPropertyValue("�����ʼ�", ref _�����ʼ�, value); }
        }

        private string _�ֻ�;
        public string �ֻ�
        {
            get { return _�ֻ�; }
            set { SetPropertyValue("�ֻ�", ref _�ֻ�, value); }
        }

        private Ա�� _ҵ��Ա;
        public Ա�� ҵ��Ա
        {
            get { return _ҵ��Ա; }
            set { SetPropertyValue("ҵ��Ա", ref _ҵ��Ա, value); }
        }

        private �����Ŷ� _�����Ŷ�;
        public �����Ŷ� �����Ŷ�
        {
            get { return _�����Ŷ�; }
            set { SetPropertyValue("�����Ŷ�", ref _�����Ŷ�, value); }
        }

        private int _���ȼ�;
        public int ���ȼ�
        {
            get { return _���ȼ�; }
            set { SetPropertyValue("���ȼ�", ref _���ȼ�, value); }
        }

        private string _��ǩ;
        public string ��ǩ
        {
            get { return _��ǩ; }
            set { SetPropertyValue("��ǩ", ref _��ǩ, value); }
        }

        private �ͻ���Դ _�ͻ���Դ;
        public �ͻ���Դ �ͻ���Դ
        {
            get { return _�ͻ���Դ; }
            set { SetPropertyValue("�ͻ���Դ", ref _�ͻ���Դ, value); }
        }

        private ;�� _;��;
        public ;�� ;��
        {
            get { return _;��; }
            set { SetPropertyValue(";��", ref _;��, value); }
        }

        private Ӫ��� _Ӫ���;
        public Ӫ��� Ӫ���
        {
            get { return _Ӫ���; }
            set { SetPropertyValue("Ӫ���", ref _Ӫ���, value); }
        }

        private bool _�ʼ�����;
        public bool �ʼ�����
        {
            get { return _�ʼ�����; }
            set { SetPropertyValue("�ʼ�����", ref _�ʼ�����, value); }
        }

        private bool _��Ч;
        public bool ��Ч
        {
            get { return _��Ч; }
            set { SetPropertyValue("��Ч", ref _��Ч, value); }
        }

        private string _�Ƽ���;
        public string �Ƽ���
        {
            get { return _�Ƽ���; }
            set { SetPropertyValue("�Ƽ���", ref _�Ƽ���, value); }
        }

        [Association,Aggregated]
        public XPCollection<����������ͨ��¼> ��ͨ��¼
        {
            get
            {
                return GetCollection<����������ͨ��¼>("��ͨ��¼");
            }
        }
    }
}