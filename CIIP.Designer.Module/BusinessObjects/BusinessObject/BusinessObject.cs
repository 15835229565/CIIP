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

    [XafDefaultProperty(nameof(Caption))]
    [XafDisplayName("�û�ҵ��")]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [DefaultClassOptions]
    public partial class BusinessObject : BusinessObjectBase
    {
        #region is persistent
        [XafDisplayName("�ɳ־û�")]
        [ToolTip("���Ƿ������ݿ��д��������Խ��ж�д��������ǳ־û��ģ���ֻ��Ϊ�������ʱʹ��.")]
        [VisibleInListView(false)]
        public bool IsPersistent
        {
            get { return GetPropertyValue<bool>(nameof(IsPersistent)); }
            set { SetPropertyValue(nameof(IsPersistent), value); }
        }
        #endregion

        #region can custom logic
        [XafDisplayName("���Զ����߼�")]
        [ModelDefault("AllowEdit", "False")]
        public bool CanCustomLogic
        {
            get { return GetPropertyValue<bool>(nameof(CanCustomLogic)); }
            set { SetPropertyValue(nameof(CanCustomLogic), value); }
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


        [XafDisplayName("������")]
        [VisibleInListView(false)]
        public bool IsCloneable
        {
            get { return GetPropertyValue<bool>(nameof(IsCloneable)); }
            set { SetPropertyValue(nameof(IsCloneable), value); }
        }


        [XafDisplayName("��������")]
        [VisibleInListView(false)]
        public bool IsVisibileInReports
        {
            get { return GetPropertyValue<bool>(nameof(IsVisibileInReports)); }
            set { SetPropertyValue(nameof(IsVisibileInReports), value); }
        }


        [XafDisplayName("���ٴ���")]
        [VisibleInListView(false)]
        public bool IsCreatableItem
        {
            get { return GetPropertyValue<bool>(nameof(IsCreatableItem)); }
            set { SetPropertyValue(nameof(IsCreatableItem), value); }
        }



        [Browsable(false)]
        public int CreateIndex { get; set; }

        public override bool CanCreateAssocication => IsRuntimeDefine;

        #endregion

        #region ctor
        public BusinessObject(Session s) : base(s)
        {

        }



        public override void AfterConstruction()
        {
            IsRuntimeDefine = true;
            IsPersistent = true;
            base.AfterConstruction();
        }
        #endregion

        [DataSourceCriteria("DomainObjectModifier != 'Sealed'")]
        [XafDisplayName("�̳�")]
        public BusinessObject Base
        {
            get { return GetPropertyValue<BusinessObject>(nameof(Base)); }
            set { SetPropertyValue(nameof(Base), value); }
        }

        public override SystemCategory SystemCategory => SystemCategory.BusinessObject;
    }

    //

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