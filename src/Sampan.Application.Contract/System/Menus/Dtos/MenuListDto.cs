using System.Collections.Generic;
using Sampan.Public.Dto;

namespace Sampan.Service.Contract.System.Menus
{
    /// <summary>
    /// 菜单列表业务实体
    /// </summary>
    public class MenuListDto : SortEnableBaseDto
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public int? PermissionId { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public string PermissionName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 重定向
        /// </summary>
        public string Redirect { get; set; }

        /// <summary>
        /// 组件
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 子级菜单
        /// </summary>
        public List<MenuListDto> Children { get; set; }
    }
}