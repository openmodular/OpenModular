namespace OpenModular.Common.Utils.Paging;

/// <summary>
/// ��ҳ��ѯ���
/// </summary>
/// <typeparam name="T">����</typeparam>
public partial class PagedResult<T>
{
    /// <summary>
    /// ��ѯ�����
    /// </summary>
    public List<T> Rows { get; set; }

    /// <summary>
    /// �ܼ�¼��
    /// </summary>
    public long Total { get; set; }

    /// <summary>
    /// ��ǰҳ��
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// ÿҳ��¼��
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// ��ҳ��
    /// </summary>
    public long TotalPage => (Total - 1 + Size) / Size;

    /// <summary>
    /// ��չ����
    /// </summary>
    public object? Extend { get; set; }

    public PagedResult()
    {
        Rows = new List<T>();
    }

    public PagedResult(List<T> rows, int total)
    {
        Rows = rows;
        Total = total;
    }

    public PagedResult(List<T> rows, int total, int index, int size)
    {
        Rows = rows;
        Total = total;
        Index = index;
        Size = size;
    }
}