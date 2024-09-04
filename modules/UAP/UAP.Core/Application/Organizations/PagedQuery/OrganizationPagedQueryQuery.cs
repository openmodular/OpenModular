using OpenModular.DDD.Core.Application.Dto;
using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Application.Organizations.Get;

namespace OpenModular.Module.UAP.Core.Application.Organizations.PagedQuery;

public record OrganizationPagedQueryQuery : PageQueryBase<PagedDto<OrganizationDto>>
{
    public string Name { get; set; }

    public string Code { get; set; }
}