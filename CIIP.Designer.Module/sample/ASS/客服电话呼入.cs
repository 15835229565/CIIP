using System;
using DevExpress.Xpo;
using CIIP.Module.BusinessObjects;
using ���û���;

namespace CRM
{
    public class �ͷ��绰���� : ����
    {
        public �ͷ��绰����(Session s) : base(s)
        {

        }

        private DateTime _����ʱ��;

        public DateTime ����ʱ��
        {
            get { return _����ʱ��; }
            set { SetPropertyValue("����ʱ��", ref _����ʱ��, value); }
        }

        //�� �� ʱ �� �� �� �� �� �� ��  ��¼�� ������  ������ �������  �طý��
        private string _ͨ������;

        [Size(-1)]
        public string ͨ������
        {
            get { return _ͨ������; }
            set { SetPropertyValue("ͨ������", ref _ͨ������, value); }
        }

        private Ա�� _������;
        public Ա�� ������
        {
            get { return _������; }
            set { SetPropertyValue("������", ref _������, value); }
        }

        [Association,Aggregated]
        public XPCollection<�ͷ���ͨ��¼> ��ͨ��¼
        {
            get { return GetCollection<�ͷ���ͨ��¼>("��ͨ��¼"); }
        }
    }
}