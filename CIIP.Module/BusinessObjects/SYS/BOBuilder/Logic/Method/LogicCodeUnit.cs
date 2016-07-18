using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using Admiral.CodeFirstView;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ���û���;

namespace IMatrix.ERP.Module.BusinessObjects.SYS.Logic
{
    [XafDefaultProperty("����")]
    public abstract class LogicCodeUnit : NameObject, ITreeNode, ITreeNodeImageProvider,ICustomValidate
    {
        public LogicCodeUnit(Session s) : base(s)
        {

        }

        public override void AfterConstruction()
        {
            this.���� = GetType().Name;
            base.AfterConstruction();
        }

        private LogicCodeUnit _ParentUnit;

        [Association]
        public LogicCodeUnit ParentUnit
        {
            get { return _ParentUnit; }
            set { SetPropertyValue("ParentUnit", ref _ParentUnit, value); }
        }

        [Association, DevExpress.Xpo.Aggregated]
        public XPCollection<LogicCodeUnit> ChildrenUnits
        {
            get { return GetCollection<LogicCodeUnit>("ChildrenUnits"); }
        }

        [Browsable(false)]
        public virtual IBindingList Children
        {
            get { return ChildrenUnits; }
        }

        [Browsable(false)]
        public virtual string Name
        {
            get { return ����; }
        }

        [Browsable(false)]
        public virtual ITreeNode Parent
        {
            get { return this.ParentUnit; }
        }

        private int _Index;

        public int Index
        {
            get { return _Index; }
            set { SetPropertyValue("Index", ref _Index, value); }
        }

        public Image GetImage(out string imageName)
        {
            imageName = CaptionHelper.ApplicationModel.BOModel.GetClass(this.GetType()).ImageName;
            return ImageLoader.Instance.GetImageInfo(imageName).Image;
        }

        

        /// <summary>
        /// ��ȡ�ɶ�����
        /// </summary>
        /// <returns></returns>
        //public virtual List<VariantDefine> GetReadableVariants()
        //{
        //    var list = new List<VariantDefine>();
        //    #region ƽ������ı���

        //    if (ParentUnit != null)
        //    {
        //        var child =
        //            ParentUnit.ChildrenUnits.OfType<VariantDefine>()
        //                .Where(x => x.Index < this.Index)
        //                .OrderBy(x => x.Index);
        //        foreach (var c in child)
        //        {
        //            var p = c as ParameterDefine;
        //            if (p == null || p.ParameterType == ParameterType.Input)
        //                list.Add(c);
        //        }
        //        list.AddRange(ParentUnit.GetReadableVariants());
        //    }

        //    #endregion

        //    return list;
        //}

        //public virtual List<VariantDefine> GetWriteableVariants()
        //{
        //    return null;
        //}

        public virtual bool IsValidate(out string message, IRuleBaseProperties properties, RuleBase rule)
        {
            message = "";
            return true;
        }
    }

    public class LogicCodeUnit_ListView : ListViewObject<LogicCodeUnit>
    {
        public override void LayoutListView()
        {
            
        }

        public override void LayoutDetailView()
        {
            DetailViewLayout.ClearNodes();
            var mst = DetailView.Items["StaticText"];
            if (mst == null)
            {
                var t = DetailView.Items.AddNode<IModelStaticText>("StaticText");
                t.Text = "���ڲ˵���[���ָ��],��ѡ����Ӧ���Ӽ���Ŀ.";
                mst = t;
            }
            var msg = DetailViewLayout.AddNode<IModelLayoutViewItem>("Message");
            msg.ViewItem = mst;
            
            base.LayoutDetailView();
        }
    }
}