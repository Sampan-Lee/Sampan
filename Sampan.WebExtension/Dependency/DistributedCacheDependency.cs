using Microsoft.Extensions.DependencyInjection;
using Sampan.Infrastructure.DistributedCache;

namespace Sampan.WebExtension.Dependency
{
    public static class DistributedCacheDependency
    {
        public static void AddDistributedCache(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IDistributedCache), typeof(RedisDistributedCache));
        }
    }
}