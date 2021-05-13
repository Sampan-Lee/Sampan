using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Sampan.WebExtension.Permission;

namespace Sampan.WebExtension.Dependency
{
    public static class AuthenticationDependency
    {
        public static void AddPolicyAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddSingleton<IAuthorizationMiddlewareResultHandler,
                PermissionAuthorizationMiddlewareResultHandler>();
        }
    }
}