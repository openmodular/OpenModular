using Microsoft.OpenApi.Models;
using OpenModular.DDD.Core.Domain.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OpenModular.Host.Web.OpenApi.Filters;

internal class TypeIdSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.BaseType == typeof(TypedIdValueBase))
        {
            schema.Type = "string";
            schema.Format = null;
        }
    }
}