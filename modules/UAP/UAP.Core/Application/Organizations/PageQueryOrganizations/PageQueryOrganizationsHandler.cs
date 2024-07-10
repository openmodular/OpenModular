using MapsterMapper;
using OpenModular.DDD.Core.Application.Dto;
using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Application.Organizations.GetOrganization;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Organizations.PageQueryOrganizations;

internal class PageQueryOrganizationsHandler(IOrganizationRepository repository, IMapper mapper) : IQueryHandler<PageQueryOrganizationsQuery, PagedDto<OrganizationDto>>
{
    public async Task<PagedDto<OrganizationDto>> Handle(PageQueryOrganizationsQuery request, CancellationToken cancellationToken)
    {
        var pagedResult = await repository.PageQueryAsync(new Domain.Organizations.Models.OrganizationQueryModel
        {
            Name = request.Name,
            Code = request.Code
        }, request.Pagination, cancellationToken);

        return pagedResult.ToPagedDto<Organization, OrganizationDto>();
    }
}