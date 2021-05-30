using System.Collections.Generic;
using Sampan.Public.Dto;

namespace Sampan.Service.Contract.System.Permissions.Dtos
{
    /// <summary>
    /// 按模块权限业务实体
    /// </summary>
    public class PermissionModuleDto
    {
        /// <summary>
        /// 模块
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public List<PermissionDto> Permissions { get; set; }
    }

    /// <summary>
    /// 权限业务实体
    /// </summary>
    public class PermissionDto : Dto
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public List<PermissionDto> Children { get; set; }
    }
}