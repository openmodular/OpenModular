namespace OpenModular.Common.Utils.Paging;

/// <summary>
/// 分页查询结果
/// </summary>
/// <typeparam name="T">类型</typeparam>
public class PagedResult<T>
{
    /// <summary>
    /// 查询结果集
    /// </summary>
    public List<T> Rows { get; set; }

    /// <summary>
    /// 总记录数
    /// </summary>
    public long Total { get; set; }

    /// <summary>
    /// 当前页码
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// 每页记录数
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// 总页数
    /// </summary>
    public long TotalPage => (Total - 1 + Size) / Size;

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