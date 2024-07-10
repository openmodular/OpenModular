using MapsterMapper;
using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Domain.Departments;

namespace OpenModular.Module.UAP.Core.Application.Departments.GetDepartment;

internal class GetDepartmentQueryHandler(IDepartmentRepository repository, IMapper mapper) : IQueryHandler<GetDepartmentQuery, DepartmentDto>
{
    public async Task<DepartmentDto> Handle(GetDepartmentQuery query, CancellationToken cancellationToken)
    {
        var user = await repository.GetAsync(query.DepartmentId, cancellationToken);
        return mapper.Map<DepartmentDto>(user);
    }
}