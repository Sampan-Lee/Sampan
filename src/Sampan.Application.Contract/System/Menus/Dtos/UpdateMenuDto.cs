using System.ComponentModel.DataAnnotations;
using Sampan.Domain.Shared.System.Menu;
using Sampan.Public.Dto;

namespace Sampan.Service.Contract.System.Menus
{
    /// <summary>
    /// 修改菜单业务实体
    /// </summary>
    public class UpdateMenuDto : SortDto
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public int? ParentId { get; set; }

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

        /// <summary>
        /// 视图组件
        /// </summary>
        public string Component { get; set; }
    }
}