using AutoMapper;
using OpenModular.Common.Utils.Paging;
using OpenModular.DDD.Core.Application.Dto;

namespace OpenModular.DDD.Core;

internal class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap(typeof(PagedResult<>), typeof(PagedDto<>));
    }
}