using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OpenModular.DDD.Core.Application.Dto;
using OpenModular.Module.Api;
using OpenModular.Module.UAP.Api.Endpoints.Organizations.GetOrganizations;
using OpenModular.Module.UAP.Core.Application.Organizations.GetOrganization;
using OpenModular.Module.UAP.Core.Application.Organizations.PageQueryOrganizations;

namespace OpenModular.Module.UAP.Api.Endpoints.Organizations.PageQueryOrganizations
{
    internal class PageQueryOrganizationsEndpoint : EndpointAbstract
    {
        public override void Mapping(TypeAdapterConfig config)
        {
            config.ForType<PageQueryOrganizationsRequest, PageQueryOrganizationsQuery>();
        }

        public override void RouteMap(RouteGroupBuilder group, IEndpointRouteBuilder app)
        {
            group.MapPost("organizations/pagequery", Execute).WithTags(EndpointTags.Organization);
        }

        public async Task<ApiResponse<PagedDto<OrganizationDto>>> Execute([FromBody] PageQueryOrganizationsRequest request, [FromServices] IMediator mediator, [FromServices] IMapper mapper)
        {
            var query = mapper.Map<PageQueryOrganizationsQuery>(request);
            var dto = await mediator.Send(query);
            return ApiResponse.Success(dto);
        }
    }
}
