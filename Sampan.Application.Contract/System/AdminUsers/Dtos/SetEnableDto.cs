using System.ComponentModel.DataAnnotations;

namespace Sampan.Service.Contract.System.SystemUsers
{
    /// <summary>
    /// 设置用户状态
    /// </summary>
    public class SetEnableDto
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
        public bool IsEnable { get; set; }
    }
}