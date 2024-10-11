using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Organizations.GetTree;

internal class OrganizationTreeGetQuery : QueryBase<OrganizationTreeDto>
{
    public OrganizationId OrganizationId { get; set; }
}