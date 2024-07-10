using OpenModular.DDD.Core.Application.Dto;
using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Application.Organizations.GetOrganization;

namespace OpenModular.Module.UAP.Core.Application.Organizations.PageQueryOrganizations;

public record PageQueryOrganizationsQuery : PageQueryBase<PagedDto<OrganizationDto>>
{
    public string Name { get; set; }

    public string Code { get; set; }
}