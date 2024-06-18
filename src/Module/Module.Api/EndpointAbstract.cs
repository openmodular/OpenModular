using Mapster;
using Microsoft.AspNetCore.Routing;

namespace OpenModular.Module.Api;

public abstract class EndpointAbstract : IEndpoint
{
    public virtual void Mapping(TypeAdapterConfig config)
    {
    }

    public abstract void RouteMap(RouteGroupBuilder group, IEndpointRouteBuilder app);
}