using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sampan.Common.Extension;

namespace Sampan.Service.Contract.Account.AdminAccounts
{
    /// <summary>
    /// 登录请求参数
    /// </summary>
    public class LoginAdminDto : IValidatableObject
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        [Required]
        public string Identifer { get; set; }

        /// <summary>
        /// 登录凭据
        /// </summary>
        [Required]
        public string Credential { get; set; }

        /// <summary>
        /// 登录方式
        /// </summary>
        [Required]
        public LoginType Type { get; set; }

        /// <summary>
        /// 自定义验证规则
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Type == LoginType.Account)
            {
                if (Identifer.IsNullOrWhiteSpace())
                {
                    yield return new ValidationResult(
                        "登录名不能为空！",
                        new[] {"LoginName"}
                    );
                }

                if (Credential.IsNullOrWhiteSpace())
                {
                    yield return new ValidationResult(
                        "密码不能为空！",
                        new[] {"Password"}
                    );
                }
            }
            else
            {
                if (Identifer.IsNullOrWhiteSpace())
                {
                    yield return new ValidationResult(
                        "手机号不能为空！",
                        new[] {"Phone"}
                    );
                }

                if (Credential.IsNullOrWhiteSpace())
                {
                    yield return new ValidationResult(
                        "验证码不能为空！",
                        new[] {"Captcha"}
                    );
                }
            }
        }
    }

    /// <summary>
    /// 登录方式
    /// </summary>
    public enum LoginType
    {
        /// <summary>
        /// 账号登录
        /// </summary>
        Account,

        /// <summary>
        /// 手机号登录
        /// </summary>
        Phone
    }
}