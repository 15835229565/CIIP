using DevExpress.Xpo;
using ���û���;

namespace CIIP.Module.BusinessObjects
{
    public class ������� : NameObject
    {
        public �������(Session s) : base(s)
        {

        }

        [Association]
        public XPCollection<ҵ�����> ҵ�����
        {
            get { return GetCollection<ҵ�����>("ҵ�����"); }
        }
    }
}