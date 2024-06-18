using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OpenModular.Module.Api;
using OpenModular.Module.UAP.Api;
using OpenModular.Module.UAP.Core.Application.Users.CreateUser;

namespace OpenModular.Module.UAP.API.Endpoints.Users.CreateUser;

internal class CreateUserEndpoint : EndpointAbstract
{
    public override void Mapping(TypeAdapterConfig config)
    {
        config.ForType<CreateUserRequest, CreateUserCommand>();
    }

    public override void RouteMap(RouteGroupBuilder group, IEndpointRouteBuilder app)
    {
        group.MapPost("users", Execute).WithTags(EndpointTags.User);
    }

    public async Task<ApiResponse<Guid>> Execute([FromBody] CreateUserRequest request, [FromServices] IMediator mediator)
    {
        var command = request.Adapt<CreateUserCommand>();
        var userId = await mediator.Send(command);
        return ApiResponse.Success(userId.Value);
    }
}