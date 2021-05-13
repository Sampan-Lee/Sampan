using System.Linq;
using Microsoft.AspNetCore.Builder;
using Sampan.Common.Util;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Sampan.WebExtension.Middleware.Pipeline
{
    public static class Swagger
    {
        /// <summary>
        /// Swagger 中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="pathBase"></param>
        public static void UseSwagger(this IApplicationBuilder app, string pathBase)
        {
            var ApiName = Appsettings.app(new string[] {"Startup", "ApiName"});
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                typeof(ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                {
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{ApiName} {version}");
                    //接口折叠起来
                    c.DocExpansion(DocExpansion.None);
                });
                //c.RoutePrefix = "";
                //嵌入静态资源 如果扩展UI 的话可以考虑使用
                //c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("ZQCY.Appointment.Extensions.index.html");
            });
        }

        enum ApiVersions
        {
            /// <summary>
            /// V1 版本
            /// </summary>
            V1 = 1,
        }
    }
}