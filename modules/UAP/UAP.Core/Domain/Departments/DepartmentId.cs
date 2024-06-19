using OpenModular.DDD.Core.Domain.Entities;

namespace OpenModular.Module.UAP.Core.Domain.Departments;

public class DepartmentId : TypedIdValueBase
{
    public DepartmentId()
    {
        
    }

    public DepartmentId(Guid id):base(id)
    {
        
    }
}