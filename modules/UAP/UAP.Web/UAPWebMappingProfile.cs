using AutoMapper;
using OpenModular.Module.UAP.Core.Application.Configs.PagedQuery;
using OpenModular.Module.UAP.Web.Models.Configs;

namespace OpenModular.Module.UAP.Web;

public class UAPWebMappingProfile : Profile
{
    public UAPWebMappingProfile()
    {
        CreateMap<ConfigPagingQueryRequest, ConfigPagedQuery>();
    }
}