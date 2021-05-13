using Sampan.Public.Dto;

namespace Sampan.Service.Contract.System.Roles
{
    /// <summary>
    /// 角色权限业务实体
    /// </summary>
    public class RolePermissionDto : BaseDto
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
    }
}