using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sampan.Public.Permission
{
    /// <summary>
    /// 权限定义管理
    /// </summary>
    public static class PermissionManager
    {
        /// <summary>
        /// 获取程序权限
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<PermissionDefinition> GetPermissionDefinition()
        {
            List<PermissionDefinition> result = new List<PermissionDefinition>();

            var assembly = Assembly.Load("Sampan.Application.Contract");
            var permissions = assembly.GetTypes()
                .AsEnumerable()
                .Where(type => type.Name.Contains("PermissionProvider"))
                .Where(type => type.IsSealed && type.IsAbstract)
                .ToList();

            permissions.ForEach(a =>
            {
                var module = a.GetField("Module").GetValue(a).ToString();

                var modulePermissions =
                    (IDictionary<string, IDictionary<string, string>>)
                    a.GetField("Permissions").GetValue(a);

                foreach (var groupPermission in modulePermissions)
                {
                    result.AddRange(groupPermission.Value.Select(p =>
                            new PermissionDefinition(
                                module,
                                p.Key,
                                p.Value,
                                groupPermission.Key == p.Key ? null : groupPermission.Key
                            )
                        )
                    );
                }
            });

            return result;
        }

        /// <summary>
        /// 权限是否存在
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public static bool HasMember(string permission)
        {
            var permissions = GetPermissionDefinition();
            return permissions.Any(a => a.Name == permission);
        }
    }
}