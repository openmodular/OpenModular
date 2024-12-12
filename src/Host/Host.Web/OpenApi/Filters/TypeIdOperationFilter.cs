using Microsoft.OpenApi.Models;
using OpenModular.DDD.Core.Domain.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OpenModular.Host.Web.OpenApi.Filters;

internal class TypeIdOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        foreach (var parameter in operation.Parameters)
        {
            var parameterDescription = context.ApiDescription.ParameterDescriptions
                .FirstOrDefault(p => p.Name == parameter.Name && p.ModelMetadata.ContainerType != null && p.ModelMetadata.ContainerType.BaseType == typeof(TypedIdValueBase));

            if (parameterDescription != null)
            {
                if (parameterDescription.ParameterDescriptor.Name.Equals("request"))
                {
                    parameter.Name = parameterDescription.Name.TrimEnd(".Value".ToCharArray());
                }
                else
                {
                    parameter.Name = parameterDescription.ParameterDescriptor.Name;
                }
                parameter.Schema.Type = "string";
                parameter.Schema.Format = null;
            }
        }
    }
}