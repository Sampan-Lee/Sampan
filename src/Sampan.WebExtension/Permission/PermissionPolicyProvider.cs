using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Sampan.Public.Permission;

namespace Sampan.WebExtension.Permission
{
    internal class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);
            if (policy != null) return policy;


            var permission = PermissionManager.HasMember(policyName);
            if (permission)
            {
                //TODO: Optimize & Cache!
                var policyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                policyBuilder.Requirements.Add(new PermissionAuthorizationRequirement(policyName));
                return policyBuilder.Build();
            }

            return await Task.FromResult<AuthorizationPolicy>(null);
        }
    }
}