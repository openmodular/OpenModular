namespace OpenModular.Module.Web.Models;

/// <summary>
/// 分页查询相应
/// </summary>
/// <typeparam name="TData"></typeparam>
public class PagedQueryResponse<TData> : ResponseBase
{
    /// <summary>
    /// 查询结果集
    /// </summary>
    public List<TData> Rows { get; set; }

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

    /// <summary>
    /// 扩展属性
    /// </summary>
    public object? Extend { get; set; }

    public PagedQueryResponse()
    {
        Rows = new List<TData>();
    }

    public PagedQueryResponse(List<TData> rows, long total)
    {
        Rows = rows;
        Total = total;
    }

    public PagedQueryResponse(List<TData> rows, long total, int index, int size)
    {
        Rows = rows;
        Total = total;
        Index = index;
        Size = size;
    }
}