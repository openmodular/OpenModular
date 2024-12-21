using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Application.Organizations.Get;

namespace OpenModular.Module.UAP.Core.Application.Organizations.PagedQuery;

public class OrganizationPagedQuery : PagedQueryBase<OrganizationDto>
{
    public string? Name { get; set; }

    public string? Code { get; set; }
}