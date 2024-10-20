using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OpenModular.Authorization.Annotations;

namespace OpenModular.Authorization;

public class OpenModularAuthorizationHandler : AuthorizationHandler<OpenModularAuthorizationRequirement>
{
    private readonly IPermissionValidator _permissionValidator;

    public OpenModularAuthorizationHandler(IPermissionValidator permissionValidator)
    {
        _permissionValidator = permissionValidator;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OpenModularAuthorizationRequirement requirement)
    {
        //此处判断一下是否已认证
        if (!context.User.Identity!.IsAuthenticated)
        {
            return;
        }

        var httpContext = (context.Resource as DefaultHttpContext)!.HttpContext;

        //排除登录即可访问的接口
        var endpoint = httpContext.GetEndpoint();
        if (endpoint!.Metadata.Any(m => m is AuthenticatedOnlyAttribute))
        {
            context.Succeed(requirement);
            return;
        }

        var routes = httpContext.Request.RouteValues;
        var httpMethod = GetHttpMethod(httpContext.Request.Method);

        //验证权限
        if (await _permissionValidator.Validate(routes, httpMethod))
        {
            context.Succeed(requirement);
        }
    }

    private HttpMethod GetHttpMethod(string method)
    {
        switch (method)
        {
            case "GET":
                return HttpMethod.Get;
            case "POST":
                return HttpMethod.Post;
            case "PUT":
                return HttpMethod.Put;
            case "DELETE":
                return HttpMethod.Delete;
            case "HEAD":
                return HttpMethod.Head;
            case "OPTIONS":
                return HttpMethod.Options;
            case "PATCH":
                return HttpMethod.Patch;
            case "TRACE":
                return HttpMethod.Trace;
            default:
                return HttpMethod.Get;
        }
    }
}