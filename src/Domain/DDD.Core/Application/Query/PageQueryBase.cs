using OpenModular.Common.Utils.Paging;

namespace OpenModular.DDD.Core.Application.Query;

/// <summary>
/// 分页查询基类
/// </summary>
/// <typeparam name="TDto"></typeparam>
public abstract class PageQueryBase<TDto> : QueryBase<TDto>
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public Pagination Pagination { get; set; } = new();
}