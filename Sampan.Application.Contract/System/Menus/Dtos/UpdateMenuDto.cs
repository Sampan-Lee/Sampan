using System.ComponentModel.DataAnnotations;
using Sampan.Domain.Shared.System.Menu;

namespace Sampan.Service.Contract.System.Menus
{
    /// <summary>
    /// 修改菜单业务实体
    /// </summary>
    public class UpdateMenuDto
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
        [Required]
        [MaxLength(MenuConst.MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
    }
}