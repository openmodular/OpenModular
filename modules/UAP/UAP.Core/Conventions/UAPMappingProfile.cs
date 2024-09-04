using AutoMapper;
using OpenModular.Module.UAP.Core.Application.Configs.PagedQuery;
using OpenModular.Module.UAP.Core.Application.Departments.Get;
using OpenModular.Module.UAP.Core.Application.Users.Get;
using OpenModular.Module.UAP.Core.Domain.Configs;
using OpenModular.Module.UAP.Core.Domain.Configs.Models;
using OpenModular.Module.UAP.Core.Domain.Departments;
using OpenModular.Module.UAP.Core.Domain.Users;

namespace OpenModular.Module.UAP.Core.Conventions;

public class UAPMappingProfile : Profile
{
    public UAPMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<Department, DepartmentDto>();

        CreateMap<ConfigPagedQuery, ConfigPagedQueryModel>();

        CreateMap<Config, ConfigDto>();
    }
}