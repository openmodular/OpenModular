using OpenModular.Persistence;
using OpenModular.Module.UAP.Core.Domain.Organizations;
using OpenModular.DDD.Core.Uow;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Repositories;

internal class OrganizationRepository(IUnitOfWork unitOfWork) : RepositoryAbstract<Organization, OrganizationId, UAPDbContext>(unitOfWork), IOrganizationRepository
{
}