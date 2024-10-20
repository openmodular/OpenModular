using AutoMapper;
using OpenModular.Common.Utils;
using OpenModular.Common.Utils.Paging;
using OpenModular.DDD.Core.Domain.Entities;

namespace OpenModular.DDD.Core.Application.Dto;

public static class PagedResultExtensions
{
    /// <summary>
    /// 将PagedResult转为PagedDto
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    public static PagedDto<TData> ToPagedDto<TEntity, TData>(this PagedResult<TEntity> result) where TEntity : IEntity where TData : class
    {
        var mapper = GlobalServiceProvider.GetRequiredService<IMapper>();
        var rows = mapper.Map<List<TData>>(result.Rows);
        return new PagedDto<TData>(rows, result.Total, result.Index, result.Size);
    }
}