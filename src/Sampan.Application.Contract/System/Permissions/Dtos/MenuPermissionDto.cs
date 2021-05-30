using System.Collections.Generic;
using Sampan.Public.Dto;

namespace Sampan.Service.Contract.System.Permissions.Dtos
{
    /// <summary>
    /// 菜单权限
    /// </summary>
    public class MenuPermissionDto
    {
        /// <summary>
        /// 模块
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 菜单权限
        /// </summary>
        public List<DropdownDto> Permissions { get; set; }
    }
}