using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Linq;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using CIIP.Persistent.BaseImpl;
using System.Collections.Generic;
using CIIP.Module.BusinessObjects.SYS.Logic;
using CIIP;

namespace CIIP.Designer
{

    [XafDefaultProperty("Caption")]
    [XafDisplayName("�û�ҵ��")]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [DefaultClassOptions]
    public partial class BusinessObject : BusinessObjectBase
    {
        #region is persistent
        private bool _IsPersistent;

        [XafDisplayName("�ɳ־û�")]
        [ToolTip("���Ƿ������ݿ��д��������Խ��ж�д��������ǳ־û��ģ���ֻ��Ϊ�������ʱʹ��.")]
        [VisibleInListView(false)]
        public bool IsPersistent
        {
            get { return _IsPersistent; }
            set { SetPropertyValue("IsPersistent", ref _IsPersistent, value); }
        }
        #endregion
        
        #region can custom logic
        private bool _CanCustomLogic;
        [XafDisplayName("���Զ����߼�")]
        [ModelDefault("AllowEdit", "False")]
        public bool CanCustomLogic
        {
            get { return _CanCustomLogic; }
            set { SetPropertyValue("CanCustomLogic", ref _CanCustomLogic, value); }
        }
        #endregion

        #region ����



        #region logic method
        //[Association, DevExpress.Xpo.Aggregated]
        //[XafDisplayName("ҵ���߼�")]
        //public XPCollection<BusinessObjectEvent> Methods
        //{
        //    get
        //    {
        //        return GetCollection<BusinessObjectEvent>("Methods");
        //    }
        //}
        #endregion

        #endregion

        #region option

        private bool? _IsCloneable;

        [XafDisplayName("������")]
        [VisibleInListView(false)]
        public bool? IsCloneable
        {
            get { return _IsCloneable; }
            set { SetPropertyValue("IsCloneable", ref _IsCloneable, value); }
        }

        private bool? _IsVisibileInReports;

        [XafDisplayName("��������")]
        [VisibleInListView(false)]
        public bool? IsVisibileInReports
        {
            get { return _IsVisibileInReports; }
            set { SetPropertyValue("IsVisibileInReports", ref _IsVisibileInReports, value); }
        }

        private bool? _IsCreatableItem;

        [XafDisplayName("���ٴ���")]
        [VisibleInListView(false)]
        public bool? IsCreatableItem
        {
            get { return _IsCreatableItem; }
            set { SetPropertyValue("IsCreatableItem", ref _IsCreatableItem, value); }
        }



        [Browsable(false)]
        public int CreateIndex { get; set; }

        #endregion
        
        #region ctor
        public BusinessObject(Session s) : base(s)
        {

        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!IsLoading && (propertyName == nameof(Name) || propertyName == nameof(Category)))
            {
                if (this.Category != null)
                    this.FullName = this.Category.FullName + "." + this.Name;
                else
                    this.FullName = this.Name;

                if (propertyName == nameof(Name) && string.IsNullOrEmpty(Caption))
                {
                    this.Caption = newValue + "";
                }
            }
            //if (propertyName == "Base" && !IsLoading 
            //    //&& !DisableCreateGenericParameterValues
            //    )
            //{
            //    Session.Delete(GenericParameterInstances);
            //    if (newValue != null)
            //    {
            //        if (!Base.IsRuntimeDefine)
            //        {
            //            var bt = ReflectionHelper.FindType(Base.FullName);
            //            if (bt.IsGenericType)
            //            {
            //                foreach (var item in bt.GetGenericArguments())
            //                {
            //                    var gp = new GenericParameterInstance(Session);
            //                    //gp.Owner = this;
            //                    gp.Name = item.Name;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            foreach (var gp in Base.GenericParameterInstances)
            //            {
            //                var ngp = new GenericParameterInstance(Session);
            //                ngp.Name = gp.Name;
            //                ngp.ParameterIndex = gp.ParameterIndex;
            //                this.GenericParameterInstances.Add(ngp);
            //            }
            //        }
            //    }
            //}
        }

        public override void AfterConstruction()
        {
            IsRuntimeDefine = true;
            this.IsPersistent = true;
            base.AfterConstruction();
        } 
        #endregion
        
    }

#warning ��Ҫ��֤�������Ʋ��������������.


#warning �˹��ܿ��Ժ���ʵ��,��ǰ����ʹ�ø��ƹ���ֱ��copy���в���
    // ҵ����������,ʹ��Attributeָ��ʹ���ĸ�����ģ��
    // ϵͳ��ʱ,�������ʹ����Attribute����,���������и���

    //[LayoutTemplate(typeof(����ģ��)] 
    //���Ͳ�������Ӧ����: ĳ����,������ϸ ��������.
    //�������,ֻ֧����������,����������ж������,�Ͱ�˳����,����ȡ��,���账��.
    //public class ĳ���� :  ......
    //{
    //}
}