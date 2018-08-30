using System.Collections.Generic;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ���û���;

namespace CIIP.Module.BusinessObjects.SYS
{
    [XafDefaultProperty("DisplayName")]    
    public abstract class PropertyBase : NameObject
    {
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [XafDisplayName("��ʾ����")]
        public string DisplayName
        {
            get
            {
                if (OwnerBusinessObject != null)
                    return this.OwnerBusinessObject.FullName + "." + this.����;
                return this.����;
            }
        }
        
        protected abstract BusinessObject OwnerBusinessObject
        {
            get;
        }

        public PropertyBase(Session s) : base(s)
        {
        }
    }
}