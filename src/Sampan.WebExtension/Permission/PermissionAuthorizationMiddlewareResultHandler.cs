using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Sampan.WebExtension.Authentication;
using Sampan.WebExtension.Model;

namespace Sampan.WebExtension.Permission
{
    public class PermissionAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy,
            PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Challenged)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(new ApiResponse(HttpStatusCode.Unauthorized).JsonResultModel));
                return;
            }

            if (authorizeResult.Forbidden)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(new ApiResponse(HttpStatusCode.Forbidden).JsonResultModel));
                return;
            }

            await next.Invoke(context);
        }
    }
}