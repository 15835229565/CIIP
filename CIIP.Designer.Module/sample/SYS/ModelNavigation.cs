using System.ComponentModel;
using System.Drawing;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base.General;

namespace CIIP.Module.BusinessObjects.SYS
{
    [DomainComponent]
    [XafDefaultProperty("Caption")]
    [XafDisplayName("�����˵�")]
    public class ModelNavigation : ITreeNode, ITreeNodeImageProvider
    {
        [XafDisplayName("·��")]
        public string Path
        {
            get
            {
                if (_item == null)
                    return "";
                return (_item as ModelNode).Path;
            }
        }
        
        private IModelNavigationItem _item;

        public ModelNavigation(IModelNavigationItem item)
        {
            _item = item;
        }
        [XafDisplayName("����")]
        public string Caption
        {
            get
            {
                if (_item == null)
                    return "";
                return _item.Caption;
            }
        }

        string ITreeNode.Name
        {
            get { return Caption; }
        }

        private ModelNavigation _Parent;
        [XafDisplayName("�ϼ�")]
        public ModelNavigation Parent { get; set; }

        private BindingList<ModelNavigation> _children;
        [XafDisplayName("�Ӽ�")]
        public BindingList<ModelNavigation> Children
        {
            get
            {
                if (_children == null)
                {
                    _children = new BindingList<ModelNavigation>();
                    if (_item != null)
                    {
                        foreach (var item in _item.Items)
                        {
                            var mn = new ModelNavigation(item);
                            _children.Add(mn);
                        }
                    }
                }
                return _children;
            }
        }

        ITreeNode ITreeNode.Parent
        {
            get { return Parent; }
        }

        IBindingList ITreeNode.Children
        {
            get { return Children; }
        }

        Image ITreeNodeImageProvider.GetImage(out string imageName)
        {
            imageName = _item.ImageName;
            return ImageLoader.Instance.GetLargeImageInfo(imageName).Image;
        }
    }
}