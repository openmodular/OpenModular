using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Domain.Departments;

namespace OpenModular.Module.UAP.Core.Application.Departments.GetDepartment;

public record GetDepartmentQuery(DepartmentId DepartmentId) : QueryBase<DepartmentDto>;