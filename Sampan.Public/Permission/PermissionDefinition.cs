using System;

namespace Sampan.Public.Permission
{
    /// <summary>
    /// 权限定义
    /// </summary>
    public class PermissionDefinition
    {
        /// <summary>
        /// 权限定义构造函数
        /// </summary>
        /// <param name="module"></param>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <param name="parentName"></param>
        public PermissionDefinition(string module, string name, string displayName, string parentName)
        {
            Module = module ?? throw new ArgumentNullException(nameof(module));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DisplayName = displayName;
            ParentName = parentName;
        }

        /// <summary>
        /// 模块
        /// </summary>
        public string Module { get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// 依赖权限名称
        /// </summary>
        public string ParentName { get; }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + $"Module:{Module}、Permission:{Name}、ParentName:{ParentName}";
        }
    }
}