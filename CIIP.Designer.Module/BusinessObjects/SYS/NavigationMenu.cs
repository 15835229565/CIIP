using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using CIIP;

namespace CIIP.Module.BusinessObjects.SYS
{
    [XafDisplayName("�����˵�")]
    public class NavigationMenu : XPLiteObject, IFlow,IRadialMenu
    {

        private string _Name;
        [Key]
        [XafDisplayName("����")]
        [RuleRequiredField]
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue("Name", ref _Name, value); }
        }
        
        public NavigationMenu(Session s) : base(s)
        {

        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            IsDesignMode = true;
        }

        private bool _IsDesignMode;
        [Browsable(false)]
        public bool IsDesignMode
        {
            get { return _IsDesignMode; }
            set { SetPropertyValue("IsDesignMode", ref _IsDesignMode, value); }
        }
        
        [VisibleInListView(false)]
        public IFlow Menu
        {
            get { return this; }
        }

        //[VisibleInListView(false)]
        //public IRadialMenu RadialMenu
        //{
        //    get { return this; }
        //}

        [Association, DevExpress.Xpo.Aggregated]
        [XafDisplayName("�˵���Ŀ")]
        public XPCollection<NavigationMenuItem> Nodes
        {
            get { return GetCollection<NavigationMenuItem>("Nodes"); }
        }

        [XafDisplayName("ָʾ�߶�")]
        [Association, DevExpress.Xpo.Aggregated]
        public XPCollection<NavigationLine> Actions
        {
            get { return GetCollection<NavigationLine>("Actions"); }
        }

        private NavigationMenuItem _SelectedNode;
        [Browsable(false)]
        public NavigationMenuItem SelectedNode
        {
            get { return _SelectedNode; }
            set { SetPropertyValue("SelectedNode", ref _SelectedNode, value); }
        }

        IFlowNode IFlow.SelectedNode
        {
            get { return SelectedNode; }

            set { SelectedNode = (NavigationMenuItem) value; }
        }

        IEnumerable<IFlowNode> IFlow.Nodes
        {
            get { return Nodes; }
        }

        IEnumerable<IFlowAction> IFlow.Actions
        {
            get { return Actions; }
        }

        IFlowAction IFlow.CreateAction(IFlowNode from, IFlowNode to)
        {
            var act = new NavigationLine(Session)
            {
                From = (NavigationMenuItem) from,
                To = (NavigationMenuItem) to,
                //Caption = "����" + to.Caption
            };
            this.Actions.Add(act);
            return act;
        }

        IFlowNode IFlow.CreateNode(int x, int y, int width, int height, string form, string caption)
        {
            var node = new NavigationMenuItem(Session)
            {
                Caption = caption,
                //Form = form,
                X = x,
                Y = y,
                Width = width,
                Height = height
            };
            this.Nodes.Add(node);
            return node;
        }

        public void RemoveAction(IFlowAction action)
        {
            Actions.Remove((NavigationLine) action);
        }

        public void RemoveNode(IFlowNode node)
        {
            Nodes.Remove((NavigationMenuItem) node);
        }

        public void ShowNodesView(ShowNodesEventParameter p)
        {
            if (!IsDesignMode)
            {
                var mi = (p.SelectedNode as NavigationMenuItem);
                if (mi != null)
                    p.DoShowNavigationItem(mi.NavigationItemPath.Path);
                return;
            }
            NavigationMenuItem obj;
            if (p.Shape == null)
            {
                obj =
                    (NavigationMenuItem)
                        (this as IFlow).CreateNode((int) p.MouseClickPoint.X, (int) p.MouseClickPoint.Y, 64, 64, "", "");
                //û�ж���ѡ�񣬵��������б�ѡ��ڵ㡣����ڵ������Ӽ��ģ����Ӽ���ʾ����������Ӽ���û���ӽ��ģ�ֱ����ʾ
                //����������view�趨�ġ�
            }
            else
            {
                obj = p.SelectedNode as NavigationMenuItem;
                //���б༭������
            }

            if (obj == null)
                throw new Exception("û��״̬����");

            var view = p.Application.CreateDetailView(p.ObjectSpace, obj, false);
            p.ViewParameter.CreatedView = view;
            p.ViewParameter.TargetWindow = TargetWindow.NewModalWindow;


            var dc = new DialogController();
            dc.Accepting += (s, p1) =>
            {
                var sp = p.Shape;
                if (p.Shape == null)
                {
                    sp = p.CreateShape(obj);
                }
                this.Nodes.Add(obj);
                p.UpdateShape(obj, sp);

                obj.Save();
            };

            dc.Cancelling += (s, p1) =>
            {
                if (p.Shape == null)
                {
                    obj.Delete();
                    p.DeletSelectedNode();
                }
                //_diagram.DeleteSelectedItems();
            };
            dc.SaveOnAccept = false;
            p.ViewParameter.Controllers.Add(dc);
        }
    }
}