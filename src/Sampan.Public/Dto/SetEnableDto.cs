using System.ComponentModel.DataAnnotations;

namespace Sampan.Public.Dto
{
    /// <summary>
    /// 设置启用/禁用状态
    /// </summary>
    public class SetEnableDto
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public bool IsEnable { get; set; }
    }
}