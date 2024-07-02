using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Api.Endpoints.Organizations.GetOrganizationTree;

/// <summary>
/// 获取组织树请求
/// </summary>
public class GetOrganizationTreeRequest
{
    /// <summary>
    /// 组织编号
    /// </summary>
    public required OrganizationId OrganizationId { get; set; }
}