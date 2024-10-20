namespace OpenModular.DDD.Core.Application.Dto;

/// <summary>
/// 分页查询数据传输对象
/// </summary>
public class PagedDto<TData> : DtoBase where TData : class
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

    public PagedDto()
    {
        Rows = new List<TData>();
    }

    public PagedDto(List<TData> rows, long total)
    {
        Rows = rows;
        Total = total;
    }

    public PagedDto(List<TData> rows, long total, int index, int size)
    {
        Rows = rows;
        Total = total;
        Index = index;
        Size = size;
    }
}