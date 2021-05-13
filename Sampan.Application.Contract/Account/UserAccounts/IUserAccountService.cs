using System.Threading.Tasks;

namespace Sampan.Service.Contract.Account.UserAccounts
{
    /// <summary>
    /// App用户授权认证
    /// </summary>
    public interface IUserAccountService : IService
    {
        /// <summary>
        /// 发送登录验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task<bool> SendCaptcha(string phone);

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Register(RegisterDto input);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UserLoginDto> LoginAsync(LoginUserDto input);
    }
}