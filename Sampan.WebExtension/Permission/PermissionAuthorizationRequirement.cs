using Microsoft.AspNetCore.Authorization;

namespace Sampan.WebExtension.Permission
{
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public string Name { get; }

        public PermissionAuthorizationRequirement(string name)
        {
            Name = name;
        }
    }
}