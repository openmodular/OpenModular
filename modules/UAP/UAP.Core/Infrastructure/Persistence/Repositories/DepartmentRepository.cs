using OpenModular.DDD.Core.Uow;
using OpenModular.Module.UAP.Core.Domain.Departments;
using OpenModular.Persistence;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Repositories;

internal class DepartmentRepository(IUnitOfWork unitOfWork) : RepositoryAbstract<Department, DepartmentId, UAPDbContext>(unitOfWork), IDepartmentRepository
{
}