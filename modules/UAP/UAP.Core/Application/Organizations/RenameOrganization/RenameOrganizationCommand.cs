using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Organizations.RenameOrganization;

public record RenameOrganizationCommand(OrganizationId OrganizationId, string Name) : CommandBase;