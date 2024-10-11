using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Organizations.Get;

public class OrganizationGetQuery : QueryBase<OrganizationDto>
{
    public OrganizationId Id { get; set; }
}