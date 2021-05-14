using System.ComponentModel.DataAnnotations;

namespace Sampan.Service.Contract.System.SystemUsers
{
    /// <summary>
    /// 重置密码参数
    /// </summary>
    public class ResetPasswordDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}