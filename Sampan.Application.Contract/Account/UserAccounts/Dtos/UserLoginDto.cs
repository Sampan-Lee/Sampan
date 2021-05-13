using Sampan.Public.Dto;

namespace Sampan.Service.Contract.Account.UserAccounts
{
    /// <summary>
    /// 用户登录结果
    /// </summary>
    public class UserLoginDto : BaseDto
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Name { get; set; }
    }
}