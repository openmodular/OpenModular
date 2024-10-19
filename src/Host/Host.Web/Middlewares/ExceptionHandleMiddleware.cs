using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenModular.DDD.Core.Domain.Exceptions;
using OpenModular.Module.Abstractions.Exceptions;
using OpenModular.Module.Abstractions.Localization;
using OpenModular.Module.Web;

namespace OpenModular.Host.Web.Middlewares
{
    internal class ExceptionHandleMiddleware(ILogger<ExceptionHandleMiddleware> logger, IHostEnvironment env, IServiceProvider service) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ModuleBusinessException ex)
            {
                logger.LogError(ex,
                    "Throw module business exception,the module code is {moduleCode},the error code is {errorCode}",
                    ex.ModuleCode, ex.ErrorCode);

                var message = ex.Message;
                var localizer = service.GetKeyedService<IModuleLocalizer>(ex.ModuleCode);
                if (localizer != null)
                {
                    message = localizer[$"ErrorCode_{ex.ErrorCode}"];
                }

                await HandleExceptionAsync(context, Convert.ToInt32(ex.ErrorCode), message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Entity not found,the id is [{id}],the entity type is [{type}]", ex.Id, ex.EntityType);

                await HandleExceptionAsync(context, 404, ex.Message);
            }
            catch (ArgumentException ex)
            {
                logger.LogError(ex, "argument exception");

                await HandleExceptionAsync(context, 400, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");

                //开发环境返回详细异常信息
                var error = env.IsDevelopment() ? ex.ToString() : ex.Message;

                await HandleExceptionAsync(context, 500, error);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, int code, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            var apiResponse = new APIResponse(code, message);

            return context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse));
        }
    }
}
