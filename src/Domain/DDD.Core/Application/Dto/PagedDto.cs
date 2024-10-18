namespace OpenModular.DDD.Core.Application.Dto;

/// <summary>
/// 分页查询数据传输对象
/// </summary>
public class PagedDto<TData>
{
    /// <summary>
    /// 查询结果集
    /// </summary>
    public List<TData> Rows { get; set; }

    /// <summary>
    /// 总记录数
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    /// 当前页码
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// 每页记录数
    /// </summary>
    public int Size { get; set; }

    public PagedDto()
    {
        Rows = new List<TData>();
    }

    public PagedDto(List<TData> rows, int total)
    {
        Rows = rows;
        Total = total;
    }

    public PagedDto(List<TData> rows, int total, int index, int size)
    {
        Rows = rows;
        Total = total;
        Index = index;
        Size = size;
    }
}