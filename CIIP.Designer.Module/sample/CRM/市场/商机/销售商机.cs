using System;
using DevExpress.Xpo;
using CIIP;
using CIIP.Module.BusinessObjects;
using ���û���;

namespace CRM
{
    public class �����̻� : SimpleObject
    {
        public �����̻�(Session s) : base(s)
        {
        }

        private string _����;
        public string ����
        {
            get { return _����; }
            set { SetPropertyValue("����", ref _����, value); }
        }

        private decimal _Ԥ������;
        public decimal Ԥ������
        {
            get { return _Ԥ������; }
            set { SetPropertyValue("Ԥ������", ref _Ԥ������, value); }
        }

        private decimal _Ԥ���������;
        public decimal Ԥ���������
        {
            get { return _Ԥ���������; }
            set { SetPropertyValue("Ԥ���������", ref _Ԥ���������, value); }
        }

        private ������λ _�ͻ�;
        public ������λ �ͻ�
        {
            get { return _�ͻ�; }
            set { SetPropertyValue("�ͻ�", ref _�ͻ�, value); }
        }

        private �������� _��������;
        public �������� ��������
        {
            get { return _��������; }
            set { SetPropertyValue("��������", ref _��������, value); }
        }

        private DateTime _�´��ж�����;
        public DateTime �´��ж�����
        {
            get { return _�´��ж�����; }
            set { SetPropertyValue("�´��ж�����", ref _�´��ж�����, value); }
        }

        private DateTime _�´��ж���������;
        public DateTime �´��ж���������
        {
            get { return _�´��ж���������; }
            set { SetPropertyValue("�´��ж���������", ref _�´��ж���������, value); }
        }

        private string _�´��ж�����;
        public string �´��ж�����
        {
            get { return _�´��ж�����; }
            set { SetPropertyValue("�´��ж�����", ref _�´��ж�����, value); }
        }
    }
}