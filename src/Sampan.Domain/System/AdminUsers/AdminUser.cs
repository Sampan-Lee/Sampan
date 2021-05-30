using System.Collections.Generic;
using FreeSql.DataAnnotations;
using JetBrains.Annotations;
using Sampan.Common.Util;

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
        /// 用户角色信息
        /// </summary>
        [Navigate(ManyToMany = typeof(UserRole))]
        public virtual ICollection<Role> Roles { get; set; }

        internal AdminUser ChangeName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name));
            return this;
        }

        internal AdminUser ChangePhone([NotNull] string phone)
        {
            Phone = Check.NotNullOrWhiteSpace(phone, nameof(phone));
            return this;
        }

        internal AdminUser ChangeEmail([CanBeNull] string email)
        {
            Email = email;
            return this;
        }
    }
}