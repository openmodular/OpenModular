using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Domain.Departments;

namespace OpenModular.Module.UAP.Core.Application.Departments.Get;

public class DepartmentGetQuery : QueryBase<DepartmentDto>
{
    public DepartmentId DepartmentId { get; set; }
}