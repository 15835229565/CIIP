using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using CIIP.Module.BusinessObjects.Flow;
using System.Linq;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using ���û���;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using CIIP.Module.BusinessObjects.SYS.Logic;
using CIIP;

namespace CIIP.Module.BusinessObjects.SYS
{

    public enum Modifier
    {
        [XafDisplayName("��ͨ")]
        None,
        [XafDisplayName("���� - ���뱻�̳�")]
        Abstract,
        [XafDisplayName("�ܷ� - �����Ա��̳�")]
        Sealed
    }

    [XafDefaultProperty("Caption")]
    [XafDisplayName("�û�ҵ��")]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
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

        #region modifier
        [XafDisplayName("�̳�����")]
        [ToolTip("��������Ϊ��,����,�ܷ��")]
        public Modifier Modifier
        {
            get
            {
                return GetPropertyValue<Modifier>(nameof(Modifier));
            }
            set
            {
                SetPropertyValue(nameof(Modifier), value);
            }
        }
        #endregion

        #region ����

        #region ���Ͳ�������
        [XafDisplayName("���Ͳ�������")]
        [ToolTip("�����Ҫ���Ͳ���ʱ,�����ڴ˶���,���������Լ�ҵ���߼���ʹ��!")]
        [Association, DevExpress.Xpo.Aggregated]
        public XPCollection<GenericParameterDefine> GenericParameterDefines
        {
            get
            {
                return GetCollection<GenericParameterDefine>(nameof(GenericParameterDefines));
            }
        } 
        #endregion

        private bool _IsGenericTypeDefine;
        [XafDisplayName("���Ͷ���")]
        [ToolTip("�����Ƿ��Ƿ��Ͷ���")]
        public bool IsGenericTypeDefine
        {
            get { return _IsGenericTypeDefine; }
            set { SetPropertyValue("IsGenericTypeDefine", ref _IsGenericTypeDefine, value); }
        }

        //[Association, DevExpress.Xpo.Aggregated]
        //[XafDisplayName("������෺�Ͳ���")]
        //public XPCollection<GenericParameterInstance> GenericParameterInstances
        //{
        //    get { return GetCollection<GenericParameterInstance>(nameof(GenericParameterInstances)); }
        //}

        //[Appearance("���෺�Ͳ����ɼ�", TargetItems = "GenericParameters", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        //protected bool GenericParameterIsVisible()
        //{
        //    if (Base != null)
        //    {
        //        if (Base.IsRuntimeDefine)
        //            return true;
        //        var bt = ReflectionHelper.FindType(Base.FullName);
        //        if (bt != null)
        //        {
        //            return !bt.IsGenericType;
        //        }
        //    }
        //    return true;
        //}

        
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

        #region �̳�
        private BusinessObject _Base;

        [XafDisplayName("�̳�")]
        [RuleRequiredField]
        [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
        public BusinessObject Base
        {
            get { return _Base; }
            set
            {
                SetPropertyValue("Base", ref _Base, value);
            }
        }

        #endregion

        #region ����
        [Association, DevExpress.Xpo.Aggregated]
        [XafDisplayName("����")]
        public XPCollection<Property> Properties
        {
            get { return GetCollection<Property>("Properties"); }
        }

        [Association, DevExpress.Xpo.Aggregated]
        [XafDisplayName("��������")]
        public XPCollection<CollectionProperty> CollectionProperties
        {
            get { return GetCollection<CollectionProperty>("CollectionProperties"); }
        }

        public PropertyBase FindProperty(string name)
        {
            var sp = Properties.SingleOrDefault(x => x.���� == name);
            if (sp != null)
            {
                return sp;
            }
            return CollectionProperties.SingleOrDefault(x => x.���� == name);
        }

        #region logic method
        [Association, DevExpress.Xpo.Aggregated]
        [XafDisplayName("ҵ���߼�")]
        public XPCollection<BusinessObjectEvent> Methods
        {
            get
            {
                return GetCollection<BusinessObjectEvent>("Methods");
            }
        }
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

        private bool _IsRuntimeDefine;

        [XafDisplayName("��̬����")]
        [ToolTip("Ϊ��ʱ��ͨ�����뷽ʽ�ϴ���ģ�����ɵġ��������ڽ����϶��岢���ɵġ�")]
        public bool IsRuntimeDefine
        {
            get { return _IsRuntimeDefine; }
            set { SetPropertyValue("IsRuntimeDefine", ref _IsRuntimeDefine, value); }
        }

        [Browsable(false)]
        public int CreateIndex { get; set; }

        #endregion

        #region ��������
        List<NavigationItem> NavigationItemDataSources
        {
            get
            {
                return ModelDataSource.NavigationItemDataSources;
            }
        }

        private NavigationItem _NavigationItem;
        [ValueConverter(typeof(ModelNavigationToStringConverter))]
        [DataSourceProperty("NavigationItemDataSources")]
        [XafDisplayName("����")]
        public NavigationItem NavigationItem
        {
            get { return _NavigationItem; }
            set { SetPropertyValue("NavigationItem", ref _NavigationItem, value); }
        }
        #endregion
        
        #region ctor
        public BusinessObject(Session s) : base(s)
        {

        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!IsLoading && (propertyName == "����" || propertyName == "Category"))
            {
                if (this.Category != null)
                    this.FullName = this.Category.FullName + "." + this.����;
                if (propertyName == "����" && string.IsNullOrEmpty(Caption))
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