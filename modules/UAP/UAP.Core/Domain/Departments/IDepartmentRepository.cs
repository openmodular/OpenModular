using OpenModular.DDD.Core.Domain.Repositories;

namespace OpenModular.Module.UAP.Core.Domain.Departments;

internal interface IDepartmentRepository : IRepository<Department, DepartmentId>;