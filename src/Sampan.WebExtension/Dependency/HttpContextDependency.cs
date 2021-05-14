using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;

namespace Sampan.WebExtension.Dependency
{
    public static class HttpContextDependency
    {
        public static void AddHttpContext(this IServiceCollection services)
        {
            //配置中间件以转接 X-Forwarded-For 和 X-Forwarded-Proto标头
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                //仅允许受信任的代理和网络转接头。否则，可能会受到 IP 欺骗攻击
                //options.KnownProxies.Add(IPAddress.Parse("172.18.0.100"));
            });
        }
    }
}