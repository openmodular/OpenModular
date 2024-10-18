using OpenModular.Common.Utils.Paging;

namespace OpenModular.DDD.Core.Application.Query;

/// <summary>
/// 定义一个分页查询请求
/// </summary>
/// <typeparam name="TDto"></typeparam>
public abstract class PageQuery<TDto> : Query<TDto>
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public Pagination Pagination { get; set; } = new();
}