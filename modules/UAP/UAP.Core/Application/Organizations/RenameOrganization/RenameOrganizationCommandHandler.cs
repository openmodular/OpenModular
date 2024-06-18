using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Organizations.RenameOrganization;

internal class RenameOrganizationCommandHandler(IOrganizationRepository repository) : ICommandHandler<RenameOrganizationCommand>
{
    public async Task Handle(RenameOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organization = await repository.GetAsync(request.OrganizationId, cancellationToken);

        organization.Rename(request.Name);

        await repository.UpdateAsync(organization, cancellationToken);
    }
}