using System;
using DevExpress.Xpo;

namespace CIIP
{
    /// <summary>
    /// �����߼�����ģ�������Ӽ��߼����½�ʱ�����Դ�����parentType��������
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ChildrenTypeAttribute : Attribute
    {
        public Type ChildrenType { get; set; }
        public ChildrenTypeAttribute(Type childrenType)
        {
            this.ChildrenType = childrenType;
        }
    }
}