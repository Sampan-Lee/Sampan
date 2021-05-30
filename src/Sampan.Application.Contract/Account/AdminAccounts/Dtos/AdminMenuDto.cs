using System.Collections.Generic;

namespace Sampan.Service.Contract.Account.AdminAccounts
{
    /// <summary>
    /// 用户菜单
    /// </summary>
    public class AdminMenuDto
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
        /// 页面组件
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 子级菜单
        /// </summary>
        public List<AdminMenuDto> Children { get; set; }
    }
}