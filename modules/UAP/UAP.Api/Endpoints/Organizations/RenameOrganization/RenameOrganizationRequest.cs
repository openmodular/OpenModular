using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Api.Endpoints.Organizations.RenameOrganization;

public record RenameOrganizationRequest
{
    public OrganizationId OrganizationId { get; set; }

    public string Name { get; set; }
}