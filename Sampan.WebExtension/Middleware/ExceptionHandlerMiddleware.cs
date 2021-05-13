using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Sampan.Common.Log;
using Sampan.WebExtension.Authentication;
using Sampan.WebExtension.Model;

namespace Sampan.WebExtension.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            if (e == null) return;

            LogHelper.Error(e, e.GetBaseException().ToString());

            await WriteExceptionAsync(context, e).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception e)
        {
            context.Response.StatusCode = e is UnauthorizedAccessException
                ? HttpStatusCode.Unauthorized
                : HttpStatusCode.BadRequest;
            
            context.Response.ContentType = "application/json";

            await context.Response
                .WriteAsync(JsonConvert.SerializeObject((new ApiResponse(HttpStatusCode.ServerError, e.Message))
                    .JsonResultModel)).ConfigureAwait(false);
        }
    }
}