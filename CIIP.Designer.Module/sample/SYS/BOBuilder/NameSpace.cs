using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Xpo;
using ���û���;

namespace CIIP.Module.BusinessObjects.SYS
{
    [XafDisplayName("ҵ�����")]
    public class NameSpace : NameObject, ITreeNode
    {
        [Association, DevExpress.Xpo.Aggregated]
        public XPCollection<NameSpace> Children
        {
            get { return GetCollection<NameSpace>("Children"); }
        }

        public override string ����
        {
            get
            {
                return base.����;
            }

            set
            {
                base.���� = value;
                if (!IsLoading)
                {
                    _fullName = null;
                }
            }
        }

        private NameSpace _Parent;
        [Association, DevExpress.Xpo.Aggregated]
        [VisibleInListView(false)]
        public NameSpace Parent
        {
            get { return _Parent; }
            set
            {
                SetPropertyValue("Parent", ref _Parent, value);
                if (!IsLoading)
                {
                    _fullName = null;
                }
            }
        }
        
        string ITreeNode.Name
        {
            get { return ����; }
        }

        ITreeNode ITreeNode.Parent
        {
            get
            {
                return Parent;
            }
        }

        IBindingList ITreeNode.Children
        {
            get { return Children; }
        }

        string _fullName;

        [ModelDefault("AllowEdit","False")]
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(_fullName))
                {
                    if (Parent != null)
                    {
                        _fullName = Parent.FullName + "." + this.����;
                    }
                    else
                    {
                        _fullName = this.����;
                    }
                }
                return _fullName;
            }
            set
            {
                if (IsLoading)
                {
                    SetPropertyValue("FullName", ref _fullName, value);
                }
            }
        }

        public NameSpace(Session s) : base(s)
        {

        }
    }
}