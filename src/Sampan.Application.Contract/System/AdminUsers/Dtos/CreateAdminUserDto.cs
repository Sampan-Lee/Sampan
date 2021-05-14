using System.ComponentModel.DataAnnotations;

namespace Sampan.Service.Contract.System.SystemUsers
{
    /// <summary>
    /// 创建用户业务实体
    /// </summary>
    public class CreateAdminUserDto
    {
        /// <summary>
        /// 登录名
        /// </summary>
        [Required]
        public string LoginName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 用户身份，是否是管理员
        /// </summary>
        public bool? IsAdmin { get; set; }
    }
}