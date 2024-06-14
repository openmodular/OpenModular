using Microsoft.AspNetCore.Http;
using OpenModular.DDD.Core.Uow;

namespace OpenModular.Host.Api.Middlewares;

public class UnitOfWorkMiddleware : IMiddleware
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkMiddleware(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    { 
        await next(context);
        await _unitOfWork.CompleteAsync();
    }
}