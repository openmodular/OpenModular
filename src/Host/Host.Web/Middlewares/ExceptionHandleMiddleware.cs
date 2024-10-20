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
    internal class ExceptionHandleMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandleMiddleware> _logger;
        private readonly IHostEnvironment _env;
        private readonly IServiceProvider _service;

        public ExceptionHandleMiddleware(ILogger<ExceptionHandleMiddleware> logger, IHostEnvironment env, IServiceProvider service)
        {
            _logger = logger;
            _env = env;
            _service = service;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ModuleBusinessException ex)
            {
                _logger.LogError(ex,
                    "Throw module business exception,the module code is {moduleCode},the error code is {errorCode}",
                    ex.ModuleCode, ex.ErrorCode);

                var message = string.Empty;
                var code = Convert.ToInt32(ex.ErrorCode);
                var localizer = _service.GetKeyedService<IModuleLocalizer>(ex.ModuleCode);
                if (localizer != null)
                {
                    message = localizer[$"ErrorCode_{code}"];
                }

                await HandleExceptionAsync(context, code, message);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex, "Entity not found,the id is [{id}],the entity type is [{type}]", ex.Id, ex.EntityType);

                var error = !_env.IsProduction() ? ex.ToString() : ex.Message;
                await HandleExceptionAsync(context, 404, error);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "argument exception");
                var error = !_env.IsProduction() ? ex.ToString() : ex.Message;
                await HandleExceptionAsync(context, 400, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");

                //开发环境返回详细异常信息
                var error = !_env.IsProduction() ? ex.ToString() : ex.Message;

                await HandleExceptionAsync(context, 500, error);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, int code, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            var apiResponse = new APIResponse(code, message);

            return context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse, JsonSerializerOptions.Web));
        }
    }
}
