using OpenModular.DDD.Core.Domain.Entities;

namespace OpenModular.Module.UAP.Core.Domain.Departments;

public sealed class DepartmentId : TypedIdValueBase
{
    public DepartmentId()
    {
    }

    public DepartmentId(string id) : base(id)
    {
    }

    public DepartmentId(Guid id) : base(id)
    {
    }
}