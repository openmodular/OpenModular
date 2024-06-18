using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OpenModular.Module.Api;
using OpenModular.Module.UAP.Core.Application.Organizations.RenameOrganization;

namespace OpenModular.Module.UAP.Api.Endpoints.Organizations.RenameOrganization;

internal class RenameOrganizationEndpoint : EndpointAbstract
{
    public override void RouteMap(RouteGroupBuilder group, IEndpointRouteBuilder app)
    {
        group.MapPost("organizations", Execute).WithTags(EndpointTags.User);
    }

    public async Task<ApiResponse> Execute([FromBody] RenameOrganizationRequest request,
        [FromServices] IMediator mediator)
    {
        var command = new RenameOrganizationCommand(request.OrganizationId, request.Name);
        await mediator.Send(command);
        return ApiResponse.Success();
    }
}