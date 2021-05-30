using Sampan.Public.Dto;

namespace Sampan.Service.Contract.System.Menus
{
    /// <summary>
    /// 菜单业务实体
    /// </summary>
    public class MenuDto : SortDto
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
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 组件
        /// </summary>
        public string Component { get; set; }
    }
}