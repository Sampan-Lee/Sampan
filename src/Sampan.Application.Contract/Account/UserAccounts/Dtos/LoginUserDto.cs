using System.ComponentModel.DataAnnotations;

namespace Sampan.Service.Contract.Account.UserAccounts
{
    /// <summary>
    /// 用户登录参数
    /// </summary>
    public class LoginUserDto
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Phone(ErrorMessage = "手机号格式不正确！")]
        public string Phone { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [StringLength(6, ErrorMessage = "请输入6位验证码")]
        public string Captcha { get; set; }
    }
}