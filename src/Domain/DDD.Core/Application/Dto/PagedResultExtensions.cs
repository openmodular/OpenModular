using Mapster;
using OpenModular.Common.Utils.Paging;

namespace OpenModular.DDD.Core.Application.Dto;

public static class PagedResultExtensions
{
    public static PagedDto<TTarget> ToPagedDto<TSource, TTarget>(this PagedResult<TSource> pagedResult)
    {
        return new PagedDto<TTarget>(pagedResult.Rows.Adapt<List<TTarget>>(), pagedResult.Total, pagedResult.Index, pagedResult.Size);
    }
}