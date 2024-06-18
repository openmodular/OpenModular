using OpenModular.DDD.Core.Domain.Repositories;

namespace OpenModular.Module.UAP.Core.Domain.Organizations;

/// <summary>
/// 组织单位仓储
/// </summary>
internal interface IOrganizationRepository : IRepository<Organization, OrganizationId>;