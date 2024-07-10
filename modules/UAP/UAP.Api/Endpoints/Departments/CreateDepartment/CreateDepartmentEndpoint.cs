using OpenModular.Module.Api;
using Microsoft.AspNetCore.Routing;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenModular.Module.UAP.Core.Application.Departments.CreateDepartment;
using OpenModular.Module.UAP.Core.Domain.Departments;

namespace OpenModular.Module.UAP.Api.Endpoints.Departments.CreateDepartment;

internal class CreateDepartmentEndpoint : EndpointAbstract
{
    public override void Mapping(TypeAdapterConfig config)
    {
        config.ForType<CreateDepartmentRequest, CreateDepartmentCommand>();
    }

    public override void RouteMap(RouteGroupBuilder group, IEndpointRouteBuilder app)
    {
        group.MapPost("departments", Execute)
            .WithTags(EndpointTags.Department)
            .WithSummary("Create Department");
    }

    public async Task<ApiResponse<DepartmentId>> Execute([FromBody] CreateDepartmentRequest request, [FromServices] IMediator mediator, [FromServices] IMapper mapper)
    {
        var command = mapper.Map<CreateDepartmentCommand>(request);

        var departmentId = await mediator.Send(command);
        return ApiResponse.Success(departmentId);
    }
}