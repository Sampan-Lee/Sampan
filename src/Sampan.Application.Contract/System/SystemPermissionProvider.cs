using System.Collections.Generic;

namespace Sampan.Service.Contract.System
{
    /// <summary>
    /// 权限定义映射
    /// </summary>
    public static class SystemPermissionProvider
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public const string Module = "系统设置";

        /// <summary>
        /// 权限字典
        /// </summary>
        public static readonly IDictionary<string, IDictionary<string, string>> Permissions =
            new Dictionary<string, IDictionary<string, string>>
            {
                {
                    SystemPermission.AdminUser.Default,
                    new Dictionary<string, string>
                    {
                        {SystemPermission.AdminUser.Default, "用户管理"},
                        {SystemPermission.AdminUser.Create, "创建"},
                        {SystemPermission.AdminUser.Edit, "编辑"},
                        {SystemPermission.AdminUser.Delete, "删除"},
                        {SystemPermission.AdminUser.AssignRole, "分配角色"},
                        {SystemPermission.AdminUser.ResetPassword, "重置密码"},
                        {SystemPermission.AdminUser.SetIsEnable, "设置状态"},
                    }
                },
                {
                    SystemPermission.Role.Default,
                    new Dictionary<string, string>
                    {
                        {SystemPermission.Role.Default, "角色管理"},
                        {SystemPermission.Role.Create, "创建"},
                        {SystemPermission.Role.Edit, "编辑"},
                        {SystemPermission.Role.Delete, "删除"},
                        {SystemPermission.Role.AssignPermission, "分配权限"}
                    }
                },
                {
                    SystemPermission.Menu.Default,
                    new Dictionary<string, string>
                    {
                        {SystemPermission.Menu.Default, "菜单管理"},
                        {SystemPermission.Menu.Create, "创建"},
                        {SystemPermission.Menu.Edit, "编辑"},
                        {SystemPermission.Menu.Delete, "删除"},
                        {SystemPermission.Menu.BindPermission, "绑定权限"},
                        {SystemPermission.Menu.SetIsEnable, "设置状态"}
                    }
                },
                {
                    SystemPermission.Log.Default,
                    new Dictionary<string, string>
                    {
                        {SystemPermission.Log.Default, "系统日志"},
                    }
                }
            };
    }
}