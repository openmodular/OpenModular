using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OpenModular.Module.Web.Conventions;

/// <summary>
/// API分组约定
/// </summary>
public class ApiExplorerGroupConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        if (controller.ControllerType.Namespace.IsNull())
            return;

        var area = controller.ControllerType.GetCustomAttribute(typeof(AreaAttribute));
        if (area != null)
        {
            controller.ApiExplorer.GroupName = ((AreaAttribute)area).RouteValue;
        }
    }
}
