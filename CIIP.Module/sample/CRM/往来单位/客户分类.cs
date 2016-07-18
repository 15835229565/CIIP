using System.ComponentModel;
using DevExpress.Persistent.Base.General;
using DevExpress.Xpo;
using ���û���;

namespace CIIP.Module.BusinessObjects
{
    public class �ͻ����� : NameObject, ITreeNode
    {
        public �ͻ�����(Session s) : base(s)
        {
        }

        private �ͻ����� _�ϼ�;

        [Association]
        public �ͻ����� �ϼ�
        {
            get { return _�ϼ�; }
            set { SetPropertyValue("�ϼ�", ref _�ϼ�, value); }
        }

        [Association, Aggregated]
        public XPCollection<�ͻ�����> �Ӽ�
        {
            get { return GetCollection<�ͻ�����>("�Ӽ�"); }
        }

        IBindingList ITreeNode.Children
        {
            get { return �Ӽ�; }
        }

        string ITreeNode.Name
        {
            get { return ����; }
        }

        ITreeNode ITreeNode.Parent
        {
            get { return �ϼ�; }
        }
    }
}