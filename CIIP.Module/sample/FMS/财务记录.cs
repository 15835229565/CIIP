using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo;
using ���û���;

namespace CIIP.Module.BusinessObjects
{
    public class �����¼ : SimpleObject
    {
        public �����¼(Session s) : base(s)
        {
        }

        private decimal _���;

        public decimal ���
        {
            get { return _���; }
            set { SetPropertyValue("���", ref _���, value); }
        }

        private IEnumerable<�������> _source;

        protected virtual IEnumerable<�������> Source
        {
            get
            {
                if (_source == null)
                {
                    var type = this.GetType().FullName;
                    _source = Session.Query<�������>().Where(
                        x => x.ҵ�����.Where(
                            t => t.Oid == type
                            ).Count() > 0
                        );
                }
                return _source;
            }
        }

        private ������� _�������;

        public ������� �������
        {
            get { return _�������; }
            set { SetPropertyValue("�������", ref _�������, value); }
        }
    }
}