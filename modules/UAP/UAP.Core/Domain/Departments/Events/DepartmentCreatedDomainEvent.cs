using OpenModular.DDD.Core.Domain.Events;

namespace OpenModular.Module.UAP.Core.Domain.Departments.Events;

/// <summary>
/// 部门创建领域事件
/// </summary>
/// <param name="department"></param>
public record DepartmentCreatedDomainEvent(Department department) : DomainEventBase;