using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Organizations.Rename;

internal class RenameOrganizationCommandHandler(IOrganizationRepository repository) : CommandHandler<RenameOrganizationCommand>
{
    public override async Task ExecuteAsync(RenameOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organization = await repository.GetAsync(request.OrganizationId, cancellationToken);

        organization.Rename(request.Name);

        await repository.UpdateAsync(organization, cancellationToken: cancellationToken);
    }
}