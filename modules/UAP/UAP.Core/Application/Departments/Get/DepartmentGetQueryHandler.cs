using AutoMapper;
using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Domain.Departments;

namespace OpenModular.Module.UAP.Core.Application.Departments.Get;

internal class DepartmentGetQueryHandler(IDepartmentRepository repository, IMapper mapper) : IQueryHandler<DepartmentGetQuery, DepartmentDto>
{
    public async Task<DepartmentDto> Handle(DepartmentGetQuery query, CancellationToken cancellationToken)
    {
        var user = await repository.GetAsync(query.DepartmentId, cancellationToken);
        return mapper.Map<DepartmentDto>(user);
    }
}