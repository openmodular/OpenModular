using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OpenModular.Module.Api;
using OpenModular.Module.UAP.Api.Endpoints.Departments.CreateDepartment;
using OpenModular.Module.UAP.Core.Domain.Departments;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OpenModular.Module.UAP.Core.Application.Departments.GetDepartment;

namespace OpenModular.Module.UAP.Api.Endpoints.Departments.GetDepartment;

internal class GetDepartmentEndpoint : EndpointAbstract
{
    public override void RouteMap(RouteGroupBuilder group, IEndpointRouteBuilder app)
    {
        group.MapGet("departments", Execute)
            .WithTags(EndpointTags.Department)
            .WithSummary("Get Department");
    }

    public async Task<ApiResponse<DepartmentDto>> Execute([FromQuery][BindRequired] Guid id, [FromServices] IMediator mediator)
    {
        var departmentDto = await mediator.Send(new GetDepartmentQuery(new DepartmentId(id)));
        return ApiResponse.Success(departmentDto);
    }
}