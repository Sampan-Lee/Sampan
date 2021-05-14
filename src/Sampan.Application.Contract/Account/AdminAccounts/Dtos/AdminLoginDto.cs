using Sampan.Public.Dto;

namespace Sampan.Service.Contract.Account.AdminAccounts
{
    /// <summary>
    /// 登录用户数据
    /// </summary>
    public class AdminLoginDto : BaseDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}