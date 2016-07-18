using DevExpress.Xpo;

namespace ���û���
{
    public abstract class Logic
    {
        public virtual void AfterConstruction(object instance, Session s)
        {

        }

        public virtual void OnSaving(object instance, Session s)
        {

        }

        public virtual void Onsaved(object instance, Session s)
        {

        }

        public virtual void OnDeleting(object instance, Session s)
        {

        }

        public virtual void OnDeleted(object instance, Session s)
        {

        }

        public virtual void OnChanged(SimpleObject simpleObject, Session session, string propertyName, object oldValue, object newValue)
        {

        }
    }
}