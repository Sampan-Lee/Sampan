using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sampan.Common.Extension;
using Sampan.Infrastructure.CurrentUser;
using Sampan.Service.Contract.Account.AdminAccounts;

namespace Sampan.WebExtension.Permission
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        private readonly IAdminAccountService _accountService;

        public PermissionAuthorizationHandler(IAdminAccountService accountService)
        {
            _accountService = accountService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionAuthorizationRequirement requirement)
        {
            var userId = context.User.FindFirst(a => a.Type == ClaimTypes.Sid)?.Value;

            if (!userId.IsNullOrWhiteSpace())
            {
                if (context.User.IsAdminUser().Value)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }

                var check = _accountService.CheckPermissionAsync(int.Parse(userId!), requirement.Name).Result;
                if (check)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}