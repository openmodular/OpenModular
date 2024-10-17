using AutoMapper;
using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Organizations.Get;

internal class OrganizationGetHandler(IOrganizationRepository repository, IMapper mapper) : QueryHandler<OrganizationGetQuery, OrganizationDto>
{
    public override async Task<OrganizationDto> ExecuteAsync(OrganizationGetQuery request, CancellationToken cancellationToken)
    {
        var org = await repository.GetAsync(request.Id, cancellationToken);

        return mapper.Map<OrganizationDto>(org);
    }
}