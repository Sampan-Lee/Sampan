using System.Collections.Generic;
using Sampan.Public.Dto;

namespace Sampan.Service.Contract.System.SystemUsers
{
    /// <summary>
    /// 用户列表
    /// </summary>
    public class AdminUserListDto : BaseDto
    {
        /// <summary>
        /// 用户名称
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
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public List<string> Roles { get; set; }
    }
}