using DevExpress.Xpo;
using ���û���;

namespace CIIP.Module.BusinessObjects
{
    public class ҵ����Ŀ : NameObject
    {
        public ҵ����Ŀ(Session s) : base(s)
        {

        }

        [Association]
        public XPCollection<��������״̬��¼> ��¼
        {
            get
            {
                return GetCollection<��������״̬��¼>("��¼");
            }
        }
        
        [Association]
        public XPCollection<����> ����
        {
            get
            {
                return GetCollection<����>("����");
            }
        }
    }
}