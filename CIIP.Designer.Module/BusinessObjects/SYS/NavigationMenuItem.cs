using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using CIIP;

namespace CIIP.Module.BusinessObjects.SYS
{
    [XafDisplayName("�˵���Ŀ")]
    [XafDefaultProperty("Caption")]
    public class NavigationMenuItem : BaseObject, IFlowNode
    {
        public NavigationMenuItem(Session s) : base(s)
        {

        }

        private NavigationMenu _NavigationMenu;

        [Browsable(false)]
        [Association]
        public NavigationMenu NavigationMenu
        {
            get { return _NavigationMenu; }
            set { SetPropertyValue("NavigationMenu", ref _NavigationMenu, value); }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!IsLoading && propertyName == "NavigationItemPath")
            {
                if (newValue != null)
                {
                    Caption = ((ModelNavigation) newValue).Caption;
                }
            }
        }

        private string _Caption;

        [XafDisplayName("����")]
        public string Caption
        {
            get { return _Caption; }
            set { SetPropertyValue("Caption", ref _Caption, value); }
        }

        private int _Height;

        [XafDisplayName("�߶�")]
        public int Height
        {
            get { return _Height; }
            set { SetPropertyValue("Height", ref _Height, value); }
        }

        private int _Width;

        [XafDisplayName("���")]
        public int Width
        {
            get { return _Width; }
            set { SetPropertyValue("Width", ref _Width, value); }
        }

        private int _X;

        [XafDisplayName("����λ��")]
        public int X
        {
            get { return _X; }
            set { SetPropertyValue("X", ref _X, value); }
        }

        private int _Y;

        [XafDisplayName("����λ��")]
        public int Y
        {
            get { return _Y; }
            set { SetPropertyValue("Y", ref _Y, value); }
        }

        private ModelNavigation _NavigationItemPath;

        [DataSourceProperty("DataSources")]
        [ValueConverter(typeof (NavigationValueConverter))]
        [XafDisplayName("������Ŀ")]
        public ModelNavigation NavigationItemPath
        {
            get { return _NavigationItemPath; }
            set { SetPropertyValue("NavigationItemPath", ref _NavigationItemPath, value); }
        }

        private List<ModelNavigation> _dataSources;

        List<ModelNavigation> DataSources
        {
            get
            {
                if (_dataSources == null)
                {
                    _dataSources = new List<ModelNavigation>();
                    foreach (
                        var n in
                            (CaptionHelper.ApplicationModel as IModelApplicationNavigationItems).NavigationItems.Items)
                    {
                        _dataSources.Add(new ModelNavigation(n));
                    }
                }
                return _dataSources;
            }
        }


        public Image GetImage()
        {
            var nav = CaptionHelper.ApplicationModel as IModelApplicationNavigationItems;
            var imgName = "BO_Unknown";
            if (NavigationItemPath != null)
            {
                var item =
                    nav.NavigationItems.AllItems.FirstOrDefault(x => (x as ModelNode).Path == NavigationItemPath.Path);
                if (item != null)
                {
                    imgName = item.ImageName;
                }
            }

            return ImageLoader.Instance.GetLargeImageInfo(imgName).Image;
        }
    }
}