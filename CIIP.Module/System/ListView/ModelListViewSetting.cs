using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;

namespace CIIP
{
    [DomainComponent]
    [XafDisplayName("�б�����")]
    public class ModelListViewSetting
    {

        public bool ��ֹ��ϸ��ͼ { get; set; }
        public bool ��ʾҳ�� { get; set; }

        public bool �༭ { get; set; }

        public NewItemRowPosition �½�λ�� { get; set; }

    }
}