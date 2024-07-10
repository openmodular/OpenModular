namespace OpenModular.Module.UAP.Api.Endpoints.Departments.CreateDepartment;

/// <summary>
/// Create Department Request
/// </summary>
public class CreateDepartmentRequest
{
    /// <summary>
    /// Organization Id
    /// </summary>
    public required Guid OrganizationId { get; init; }

    /// <summary>
    /// Department Name
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Parent Department Id
    /// </summary>
    public Guid ParentId { get; init; }

    /// <summary>
    /// Department Order
    /// </summary>
    public int Order { get; init; }
}