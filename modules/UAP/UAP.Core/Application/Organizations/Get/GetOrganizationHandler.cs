using AutoMapper;
using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Organizations.Get;

internal class GetOrganizationHandler(IOrganizationRepository repository, IMapper mapper) : QueryHandler<GetOrganizationQuery, OrganizationDto>
{
    public override async Task<OrganizationDto> ExecuteAsync(GetOrganizationQuery request, CancellationToken cancellationToken)
    {
        var org = await repository.GetAsync(request.Id, cancellationToken);

        return mapper.Map<OrganizationDto>(org);
    }
}