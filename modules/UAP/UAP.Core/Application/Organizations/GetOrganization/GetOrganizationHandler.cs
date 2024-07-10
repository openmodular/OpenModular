using MapsterMapper;
using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Organizations.GetOrganization;

internal class GetOrganizationHandler(IOrganizationRepository repository, IMapper mapper) : IQueryHandler<GetOrganizationQuery, OrganizationDto>
{
    public async Task<OrganizationDto> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
    {
        var org = await repository.GetAsync(request.Id, cancellationToken);

        return mapper.Map<OrganizationDto>(org);
    }
}