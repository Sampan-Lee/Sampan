namespace Sampan.Service.Contract.System
{
    /// <summary>
    /// 权限定义
    /// </summary>
    public static class SystemPermission
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public const string Module = "System";

        /// <summary>
        /// 用户权限
        /// </summary>
        public static class AdminUser
        {
            /// <summary>
            /// 默认
            /// </summary>
            public const string Default = Module + ".User";

            /// <summary>
            /// 创建
            /// </summary>
            public const string Create = Default + ".Create";

            /// <summary>
            /// 编辑
            /// </summary>
            public const string Edit = Default + ".Edit";

            /// <summary>
            /// 删除
            /// </summary>
            public const string Delete = Default + ".Delete";

            /// <summary>
            /// 分配角色
            /// </summary>
            public const string AssignRole = Default + ".AssignRole";

            /// <summary>
            /// 重置密码
            /// </summary>
            public const string ResetPassword = Default + ".ResetPassword";

            /// <summary>
            /// 重置密码
            /// </summary>
            public const string SetIsEnable = Default + ".SetIsEnable";
        }

        /// <summary>
        /// 角色权限
        /// </summary>
        public static class Role
        {
            /// <summary>
            /// 默认
            /// </summary>
            public const string Default = Module + ".Role";

            /// <summary>
            /// 创建
            /// </summary>
            public const string Create = Default + ".Create";

            /// <summary>
            /// 编辑
            /// </summary>
            public const string Edit = Default + ".Edit";

            /// <summary>
            /// 删除
            /// </summary>
            public const string Delete = Default + ".Delete";

            /// <summary>
            /// 分配权限
            /// </summary>
            public const string AssignPermission = Default + ".AssignPermission";
        }

        /// <summary>
        /// 菜单权限
        /// </summary>
        public static class Menu
        {
            /// <summary>
            /// 默认
            /// </summary>
            public const string Default = Module + ".Menu";

            /// <summary>
            /// 创建
            /// </summary>
            public const string Create = Default + ".Create";

            /// <summary>
            /// 编辑
            /// </summary>
            public const string Edit = Default + ".Edit";

            /// <summary>
            /// 删除
            /// </summary>
            public const string Delete = Default + ".Delete";

            /// <summary>
            /// 设置状态
            /// </summary>
            public const string SetIsEnable = Default + ".SetIsEnable";

            /// <summary>
            /// 绑定权限
            /// </summary>
            public const string BindPermission = Default + ".BindPermission";
        }

        /// <summary>
        /// 用户权限
        /// </summary>
        public static class Log
        {
            /// <summary>
            /// 默认
            /// </summary>
            public const string Default = Module + ".Log";

            /// <summary>
            /// 默认
            /// </summary>
            public const string Export = Default + ".Export";
        }
    }
}