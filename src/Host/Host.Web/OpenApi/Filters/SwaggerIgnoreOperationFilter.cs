using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OpenModular.Host.Web.OpenApi.Filters
{
    public class SwaggerIgnoreOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var ignoredProperties = context.MethodInfo.GetParameters().SelectMany(p => p.ParameterType.GetProperties().Where(prop => prop.GetCustomAttribute<SwaggerIgnoreAttribute>() != null)).ToList();

            foreach (var property in ignoredProperties)
            {
                //只处理一级属性，复杂类型不处理了，费脑子~
                operation.Parameters = operation.Parameters.Where(p => !p.Name.Split('.')[0].StartsWith(property.Name, StringComparison.InvariantCulture)).ToList();
            }
        }
    }
}
