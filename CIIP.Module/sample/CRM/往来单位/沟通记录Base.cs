using System;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using CIIP;
using ���û���;

namespace CRM
{
    [NonPersistent]
    public class ��ͨ��¼Base : SimpleObject
    {
        private DateTime _��ͨʱ��;
        private string _��ͨ����;

        public ��ͨ��¼Base(Session s) : base(s)
        {

        }

        public DateTime ��ͨʱ��
        {
            get { return _��ͨʱ��; }
            set { SetPropertyValue("��ͨʱ��", ref _��ͨʱ��, value); }
        }

        [Size(-1)]
        [EditorAlias(EditorAliases.HtmlPropertyEditor)]
        public string ��ͨ����
        {
            get { return _��ͨ����; }
            set { SetPropertyValue("��ͨ����", ref _��ͨ����, value); }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.��ͨʱ�� = DateTime.Now;
        }
    }
}