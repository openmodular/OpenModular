using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Organizations.Rename;

/// <summary>
/// 重命名组织命令
/// </summary>
public class RenameOrganizationCommand : Command
{
    /// <summary>
    /// 组织编号
    /// </summary>
    public OrganizationId OrganizationId { get; set; }

    /// <summary>
    /// 组织名称
    /// </summary>
    public string Name { get; set; }
}