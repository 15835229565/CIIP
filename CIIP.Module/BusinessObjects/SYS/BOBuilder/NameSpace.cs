using System;
using System.ComponentModel;
using System.Drawing;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Xpo;
using ���û���;
using DevExpress.ExpressApp.Utils;

namespace CIIP.Module.BusinessObjects.SYS
{
    [XafDisplayName("ҵ�����")]
    public class Namespace : NameObject, ITreeNode,ITreeNodeImageProvider
    {
        [Association, DevExpress.Xpo.Aggregated]
        public XPCollection<Namespace> Children
        {
            get { return GetCollection<Namespace>("Children"); }
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

        private Namespace _Parent;
        [Association, DevExpress.Xpo.Aggregated]
        [VisibleInListView(false)]
        public Namespace Parent
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

        public Namespace(Session s) : base(s)
        {

        }

        Image ITreeNodeImageProvider.GetImage(out string imageName)
        {
            imageName = "BO_Folder";
            return ImageLoader.Instance.GetImageInfo(imageName).Image;
        }
    }
}