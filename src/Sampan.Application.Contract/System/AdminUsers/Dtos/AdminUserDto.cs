using Sampan.Public.Dto;

namespace Sampan.Service.Contract.System.SystemUsers
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class AdminUserDto : BaseDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Avatar { get; set; }
    }
}