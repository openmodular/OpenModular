using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Organizations.Rename;

internal class OrganizationRenameCommandHandler(IOrganizationRepository repository) : CommandHandler<OrganizationRenameCommand>
{
    public override async Task ExecuteAsync(OrganizationRenameCommand request, CancellationToken cancellationToken)
    {
        var organization = await repository.GetAsync(request.OrganizationId, cancellationToken);

        organization.Rename(request.Name);

        await repository.UpdateAsync(organization, cancellationToken: cancellationToken);
    }
}