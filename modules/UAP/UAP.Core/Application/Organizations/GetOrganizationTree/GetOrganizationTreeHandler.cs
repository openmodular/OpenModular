using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Domain.Departments;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Organizations.GetOrganizationTree;

internal class GetOrganizationTreeHandler : IQueryHandler<GetOrganizationTreeQuery, OrganizationTreeDto>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public GetOrganizationTreeHandler(IOrganizationRepository organizationRepository, IDepartmentRepository departmentRepository)
    {
        _organizationRepository = organizationRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<OrganizationTreeDto> Handle(GetOrganizationTreeQuery request, CancellationToken cancellationToken)
    {
        var org = await _organizationRepository.GetAsync(request.OrganizationId, cancellationToken);
        var departments = await _departmentRepository.QueryByCodePrefixAsync(org.Code, cancellationToken);

        var tree = new OrganizationTreeDto(org.Id, org.Name);
    }
}