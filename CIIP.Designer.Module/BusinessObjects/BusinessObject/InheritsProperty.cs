using DevExpress.ExpressApp.DC;

namespace CIIP.Designer
{
    [XafDisplayName("�̳�����")]
    [DomainComponent]
    public class InheritsProperty
    {
        public InheritsProperty(string name, string typeName, string owner)
        {

        }
        [XafDisplayName("����")]
        public string Name { get; set; }

        [XafDisplayName("����")]
        public string TypeName { get; set; }

        [XafDisplayName("��������")]
        public string Owner { get; set; }

    }
}