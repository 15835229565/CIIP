using CIIP.Module.BusinessObjects.SYS.Logic;
using DevExpress.ExpressApp.DC;
using DevExpress.Xpo;

namespace CIIP.Module.BusinessObjects.SYS
{
    [DomainComponent]
    public class Solution
    {
        //������ʾ����༭��
        [Size(-1)]
        public CsharpCode Code { get; set; }
    }
}