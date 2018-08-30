using System;
using CIIP.CodeFirstView;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Linq;

namespace CIIP.Module.BusinessObjects.SYS.Logic
{
    [XafDisplayName("���󴴽�")]
    public class ObjectAfterConstruction : BusinessObjectEvent
    {
        public ObjectAfterConstruction(Session s) : base(s)
        {
            
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.CodeName = "AfterConstruction";
            this.AccessorModifier = AccessorModifier.Public;
            this.MethodModifier = MethodModifier.Override;
            this.Code = "base.AfterConstruction();";
        }

        public override string ����
        {
            get { return "���󴴽�"; }

            set { base.���� = value; }
        }
        
    }

    //Ŀ��:���¼������ϸ��ͼ����ͳһ�Ű�

    public abstract class EventListViewBase<T> : ListViewObject<T>
        where T : BusinessObjectEvent
    {
        public override void LayoutListView()
        {

        }

        public override void LayoutDetailView()
        {
            DetailViewLayout.ClearNodes();
            //var mst = DetailView.Items["StaticText"];
            //if (mst == null)
            //{
            //    var t = DetailView.Items.AddNode<IModelStaticText>("StaticText");
            //    t.Text = "���ڲ˵���[���ָ��],��ѡ����Ӧ���Ӽ���Ŀ.";
            //    mst = t;
            //}
            //var msg = DetailViewLayout.AddNode<IModelLayoutViewItem>("Message");
            //msg.ViewItem = mst;
            var group = HGroup(100, x => x.Code);

            var item = group.First() as IModelLayoutViewItem;
            item.ShowCaption = false;
            
            base.LayoutDetailView();
        }
    }

    //public class MethodDefine_ListView : ListViewObject<MethodDefine>
    //{
    //    public override void LayoutListView()
    //    {

    //    }

    //    public override void LayoutDetailView()
    //    {
    //        DetailViewLayout.ClearNodes();
    //        this.HGroup(10, x => x.Code);
    //        base.LayoutDetailView();
    //    }
    //}

    public class ObjectAfterConstruction_ListView : EventListViewBase<ObjectAfterConstruction>
    {

    }

    public class ObjectDeletingEvent_ListView : EventListViewBase<ObjectDeletingEvent>
    {

    }

    public class ObjectPropertyChangedEvent_ListView : EventListViewBase<ObjectPropertyChangedEvent>
    {

    }

    public class ObjectSavedEvent_ListView : EventListViewBase<ObjectSavedEvent>
    {

    }

    public class ObjectSavingEvent_ListView : EventListViewBase<ObjectSavingEvent>
    {

    }
}