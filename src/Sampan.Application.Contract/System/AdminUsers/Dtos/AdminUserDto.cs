using System.Collections.Generic;
using Sampan.Public.Dto;

namespace Sampan.Service.Contract.System.SystemUsers
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class AdminUserDto : BaseDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public List<int> RoleIds { get; set; }
    }
}