namespace CIIP.Module.BusinessObjects.SYS
{
    /// <summary>
    /// �Ƿ��Ǿֲ�����
    /// </summary>
    public interface IPartCodeProvider
    {
        /// <summary>
        /// �滻�����´��뵽�������ĵ���ȥ
        /// </summary>
        /// <param name="allcode">�ĵ�����</param>
        /// <param name="newCode">��������</param>
        /// <returns></returns>
        string ReplaceNewCode(string allcode, string newCode);

        string DefaultLocation { get; }
    }
}