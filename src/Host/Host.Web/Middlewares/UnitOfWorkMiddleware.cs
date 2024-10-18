using Microsoft.AspNetCore.Http;
using OpenModular.DDD.Core.Uow;

namespace OpenModular.Host.Web.Middlewares;

public class UnitOfWorkMiddleware(IUnitOfWorkManager unitOfWorkManager) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var uow = unitOfWorkManager.Begin();
        await next(context);
        await uow.CompleteAsync();
    }
}