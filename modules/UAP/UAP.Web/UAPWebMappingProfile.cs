using AutoMapper;
using OpenModular.Authentication.Abstractions;
using OpenModular.Module.UAP.Core.Application.Auth.Authenticate;
using OpenModular.Module.UAP.Core.Application.Configs.PagedQuery;
using OpenModular.Module.UAP.Web.Models.Configs;
using LoginRequest = OpenModular.Module.UAP.Web.Models.Auths.LoginRequest;

namespace OpenModular.Module.UAP.Web;

public class UAPWebMappingProfile : Profile
{
    public UAPWebMappingProfile()
    {
        CreateMap<ConfigPagingQueryRequest, ConfigPagedQuery>();

        CreateMap<LoginRequest, AuthenticateCommand>()
            .ForMember(x => x.Source, y => y.MapFrom(m => AuthenticationSource.GetOrCreate(m.Source)))
            .ForMember(x => x.Terminal, y => y.MapFrom(m => AuthenticationTerminal.GetOrCreate(m.Terminal)));
    }
}