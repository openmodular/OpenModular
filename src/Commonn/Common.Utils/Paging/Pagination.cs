namespace OpenModular.Common.Utils.Paging;

/// <summary>
/// 分页查询
/// </summary>
public class Pagination
{
    /// <summary>
    /// 当前页码
    /// </summary>
    public int Index { get; set; } = 1;

    /// <summary>
    /// 每页记录数
    /// </summary>
    public int Size { get; set; } = 15;

    /// <summary>
    /// 排序
    /// </summary>
    public string? OrderBy { get; set; }
}