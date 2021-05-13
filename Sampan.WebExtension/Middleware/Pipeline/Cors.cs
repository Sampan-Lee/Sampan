using Microsoft.AspNetCore.Builder;
using Sampan.Common.Util;

namespace Sampan.WebExtension.Middleware.Pipeline
{
    /// <summary>
    /// 跨域
    /// </summary>
    public static class Cors
    {
        public static void UseCors(IApplicationBuilder app)
        {
            //跨域配置
            app.UseCors(Appsettings.app("Startup", "Cors", "Policy"));
        }
    }
}