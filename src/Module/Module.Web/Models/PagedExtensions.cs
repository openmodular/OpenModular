using AutoMapper;
using OpenModular.Common.Utils;
using OpenModular.Common.Utils.Paging;
using OpenModular.DDD.Core.Application.Dto;

namespace OpenModular.Module.Web.Models;

public static class PagedExtensions
{
    /// <summary>
    /// 将PagedDto转为PagedResponse
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <param name="pagedDto"></param>
    /// <returns></returns>
    public static APIResponse<PagedQueryResponse<TData>> ToPagedResponse<TDto, TData>(this PagedDto<TDto> pagedDto) where TDto : class where TData : class
    {
        var mapper = GlobalServiceProvider.GetRequiredService<IMapper>();
        var rows = mapper.Map<List<TData>>(pagedDto.Rows);
        var response = new PagedQueryResponse<TData>(rows, pagedDto.Total, pagedDto.Index, pagedDto.Size)
        {
            Extend = pagedDto.Extend
        };

        return APIResponse.Success(response);
    }

    /// <summary>
    /// 将PagedResult转为PagedResponse
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <param name="pagedResult"></param>
    /// <returns></returns>
    public static APIResponse<PagedQueryResponse<TData>> ToPagedResponse<TDto, TData>(this PagedResult<TDto> pagedResult) where TDto : class where TData : class
    {
        var mapper = GlobalServiceProvider.GetRequiredService<IMapper>();
        var rows = mapper.Map<List<TData>>(pagedResult.Rows);
        var response = new PagedQueryResponse<TData>(rows, pagedResult.Total, pagedResult.Index, pagedResult.Size)
        {
            Extend = pagedResult.Extend
        };

        return APIResponse.Success(response);
    }
}