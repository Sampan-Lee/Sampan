using System.Collections.Generic;
using FreeSql.DataAnnotations;

namespace Sampan.Domain.System
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Table(Name = "SystemMenu")]
    public class Menu : SortEnableBaseEntity
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public int? PermissionId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 页面组件
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 父级菜单
        /// </summary>
        [Navigate(nameof(ParentId))]
        public Menu Parent { get; set; }

        /// <summary>
        /// 子级菜单
        /// </summary>
        [Navigate(nameof(ParentId))]
        public ICollection<Menu> Children { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        [Navigate(nameof(PermissionId))]
        public Permission Permission { get; set; }
    }
}