using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Sampan.Common.Util
{
    /// <summary>
    /// 获取上下文的ip
    /// </summary>
    public static class HttpContextUtil
    {
        public static string GetClientIP(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].ToString();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }

            //反向代理获取真实Ip
            if (context.Request.Headers.ContainsKey("X-Real-IP"))
            {
                ip = context.Request.Headers["X-Real-IP"].ToString();
            }

            return context.Connection.RemoteIpAddress.ToString();
        }

        /// <summary>
        /// 获取userid
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int? GetUserId(this HttpContext context)
        {
            return context.User.FindFirst(ClaimTypes.Sid)?.Value.ToInt();
        }
    }
}