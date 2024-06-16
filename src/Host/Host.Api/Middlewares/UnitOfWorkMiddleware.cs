using Microsoft.AspNetCore.Http;
using OpenModular.DDD.Core.Uow;

namespace OpenModular.Host.Api.Middlewares;

public class UnitOfWorkMiddleware(IUnitOfWork unitOfWork) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    { 
        await next(context);
        await unitOfWork.CompleteAsync();
    }
}