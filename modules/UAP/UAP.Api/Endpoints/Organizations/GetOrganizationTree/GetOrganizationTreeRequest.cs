using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Api.Endpoints.Organizations.GetOrganizationTree;

public record GetOrganizationTreeRequest
{
    public OrganizationId OrganizationId { get; set; }
}