using AutoMapper;
using OpenModular.Authentication.Abstractions;
using OpenModular.Module.UAP.Core.Application.Accounts.Get;
using OpenModular.Module.UAP.Core.Application.Authentications.Authenticate;
using OpenModular.Module.UAP.Core.Application.Configs.PagedQuery;
using OpenModular.Module.UAP.Core.Application.Departments.Get;
using OpenModular.Module.UAP.Core.Domain.Accounts;
using OpenModular.Module.UAP.Core.Domain.Configs;
using OpenModular.Module.UAP.Core.Domain.Configs.Models;
using OpenModular.Module.UAP.Core.Domain.Departments;

namespace OpenModular.Module.UAP.Core.Conventions;

public class UAPMappingProfile : Profile
{
    public UAPMappingProfile()
    {
        CreateMap<Account, AccountDto>();
        CreateMap<Department, DepartmentDto>();

        CreateMap<ConfigPagedQuery, ConfigPagedQueryModel>();

        CreateMap<Config, ConfigDto>();

        CreateMap<AuthenticationContext<Account>, AuthenticateDto>();
    }
}