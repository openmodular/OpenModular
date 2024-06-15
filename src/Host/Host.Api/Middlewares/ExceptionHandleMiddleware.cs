using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OpenModular.Module.Abstractions.Exceptions;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using OpenModular.DDD.Core.Domain.Exceptions;

namespace OpenModular.Host.Api.Middlewares
{
    internal class ExceptionHandleMiddleware(ILogger<ExceptionHandleMiddleware> logger, IHostEnvironment env) : IMiddleware
    {
        private readonly ILogger _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ModuleBusinessException ex)
            {
                _logger.LogError(ex, "Throw module business exception,the module code is {moduleCode},the error code is {errorCode}", ex.ModuleCode, ex.ErrorCode);

                await HandleExceptionAsync(context, ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex, "Entity not found,the id is [{id}],the entity type is [{type}]", ex.Id, ex.EntityType);

                await HandleExceptionAsync(context, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");

                //开发环境返回详细异常信息
                var error = env.IsDevelopment() ? ex.ToString() : ex.Message;

                await HandleExceptionAsync(context, error);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, string error)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            return context.Response.WriteAsync(JsonSerializer.Serialize(new { success = true, message = error }));
        }
    }
}
