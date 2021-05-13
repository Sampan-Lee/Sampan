using System.Collections.Generic;

namespace Sampan.Service.Contract.System.Menus
{
    /// <summary>
    /// 用户菜单
    /// </summary>
    public class UserMenuDto
    {
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
        /// 子级菜单
        /// </summary>
        public List<UserMenuDto> Children { get; set; }
    }
}