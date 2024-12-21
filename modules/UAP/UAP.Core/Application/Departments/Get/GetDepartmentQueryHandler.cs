using AutoMapper;
using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Domain.Departments;

namespace OpenModular.Module.UAP.Core.Application.Departments.Get;

internal class GetDepartmentQueryHandler(IDepartmentRepository repository, IMapper mapper) : QueryHandler<GetDepartmentQuery, DepartmentDto>
{
    public override async Task<DepartmentDto> ExecuteAsync(GetDepartmentQuery query, CancellationToken cancellationToken)
    {
        var user = await repository.GetAsync(query.DepartmentId, cancellationToken);
        return mapper.Map<DepartmentDto>(user);
    }
}