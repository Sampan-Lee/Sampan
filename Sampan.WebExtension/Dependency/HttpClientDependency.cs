using System;
using Microsoft.Extensions.DependencyInjection;
using Sampan.Common.Util;

namespace Sampan.WebExtension.Dependency
{
    public static class HttpClientDependency
    {
        public static void AddHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("Upload",
                options =>
                {
                    options.BaseAddress = new Uri(Appsettings.app("AppSettings", "Upload", "IntranetAddress"));
                }
            );
        }
    }
}