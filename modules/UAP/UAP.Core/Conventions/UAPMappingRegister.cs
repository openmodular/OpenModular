using Mapster;
using OpenModular.Module.UAP.Core.Application.Departments.GetDepartment;
using OpenModular.Module.UAP.Core.Application.Users.GetUser;
using OpenModular.Module.UAP.Core.Domain.Departments;
using OpenModular.Module.UAP.Core.Domain.Users;

namespace OpenModular.Module.UAP.Core.Conventions;

public class UAPMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Department, DepartmentDto>();
        config.ForType<User, UserDto>();
    }
}