using System.Collections.Generic;
using System.Threading.Tasks;
using Sampan.Service.Contract.System.Menus;
using Sampan.Service.Contract.System.SystemUsers;

namespace Sampan.Service.Contract.Account.AdminAccounts
{
    /// <summary>
    /// 管理员用户授权认证
    /// </summary>
    public interface IAdminAccountService : IService
    {
        /// <summary>
        /// 发送登录验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task<bool> SendCaptcha(string phone);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminLoginDto> LoginAsync(LoginAdminDto input);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        Task<AdminUserDto> GetUserAsync();

        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <returns></returns>
        Task<List<UserMenuDto>> GetMenuAsync();

        /// <summary>
        /// 校验账号权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        Task<bool> CheckPermissionAsync(int userId, string permission);
    }
}