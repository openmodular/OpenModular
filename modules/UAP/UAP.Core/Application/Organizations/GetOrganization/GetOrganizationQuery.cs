using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Organizations.GetOrganization;

public record GetOrganizationQuery : QueryBase<OrganizationDto>
{
    public OrganizationId Id { get; set; }
}