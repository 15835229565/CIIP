using System;
using CIIP.StateMachine;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ���û���;

namespace CIIP.Module.BusinessObjects
{
    public class ״̬�����¼ : BaseObject
    {
        public ״̬�����¼(Session s) : base(s)
        {

        }

        private ���� _����;
        [Association]
        public ���� ����
        {
            get { return _����; }
            set { SetPropertyValue("����", ref _����, value); }
        }



        private DateTime _��������;

        public DateTime ��������
        {
            get { return _��������; }
            set { SetPropertyValue("��������", ref _��������, value); }
        }



        private CIIPXpoState _��Դ״̬;

        public CIIPXpoState ��Դ״̬
        {
            get { return _��Դ״̬; }
            set { SetPropertyValue("��Դ״̬", ref _��Դ״̬, value); }
        }

        private CIIPXpoState _Ŀ��״̬;

        public CIIPXpoState Ŀ��״̬
        {
            get { return _Ŀ��״̬; }
            set { SetPropertyValue("Ŀ��״̬", ref _Ŀ��״̬, value); }
        }

        private string _������;

        public string ������
        {
            get { return _������; }
            set { SetPropertyValue("������", ref _������, value); }
        }
    }
}