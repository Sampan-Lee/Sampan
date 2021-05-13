using System.Collections.Generic;
using FreeSql.DataAnnotations;
using Sampan.Public.Entity;

namespace Sampan.Domain.System
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [Table(Name = "SystemUser")]
    public class AdminUser : EnableBaseEntity
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 密码盐
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

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
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 用户角色信息
        /// </summary>
        [Navigate(ManyToMany = typeof(UserRole))]
        public virtual ICollection<Role> Roles { get; set; }
    }
}