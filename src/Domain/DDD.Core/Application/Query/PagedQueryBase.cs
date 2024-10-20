using OpenModular.Common.Utils.Paging;
using OpenModular.DDD.Core.Application.Dto;

namespace OpenModular.DDD.Core.Application.Query;

/// <summary>
/// 分页查询基类
/// </summary>
/// <typeparam name="TData"></typeparam>
public abstract class PagedQueryBase<TData> : Query<PagedDto<TData>> where TData : class
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public Pagination Pagination { get; set; } = new();
}