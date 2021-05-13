using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Longbow.Security.Cryptography;
using Sampan.Application;
using Sampan.Application.System;
using Sampan.Common.Extension;
using Sampan.Common.Util;
using Sampan.Domain.System;
using Sampan.Infrastructure.Repository;
using Sampan.Infrastructure.Sms;
using Sampan.Service.Contract.Account.AdminAccounts;
using Sampan.Service.Contract.System.Menus;
using Sampan.Service.Contract.System.SystemUsers;

namespace Sampa.Application.Account.AdminAccounts
{
    /// <summary>
    /// 管理员用户授权认证
    /// </summary>
    public class AdminAccountService : Service, IAdminAccountService
    {
        private const string _adminLoginCaptchaCacheKey = "admin_login_captcha_";
        private readonly IRepository<AdminUser> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<Menu> _menuRepository;
        private readonly ISmsService _smsService;

        public AdminAccountService(IRepository<AdminUser> userRepository,
            IRepository<Role> roleRepository,
            IRepository<Menu> menuRepository,
            ISmsService smsService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _menuRepository = menuRepository;
            _smsService = smsService;
        }

        /// <summary>
        /// 发送登录验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<bool> SendCaptcha(string phone)
        {
            var cacheKey = _adminLoginCaptchaCacheKey + phone;
            var exist = await Cache.ExistAsync(cacheKey);
            ThrowIf(exist, new BusinessException("验证码有效期5分钟，请勿重复发送"));

            var smsType = (SmsType) Appsettings.app("AppSettings", "Sms", "SmsType").ToInt();
            var captcha = smsType == SmsType.DevSms ? "666666" : new Random().Next(100000, 999999).ToString();
            await Cache.SetAsync(cacheKey, captcha, TimeSpan.FromMinutes(5));

            var send = await _smsService.SendAsync(phone, captcha);
            return send;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminLoginDto> LoginAsync(LoginAdminDto input)
        {
            AdminUser user;
            if (input.Type == LoginType.Account)
            {
                user = await _userRepository
                    .Where(a => a.LoginName == input.Identifer)
                    .ToOneAsync();

                ThrowIf(user == null, new AdminUserNotExistsException(input.Identifer));
                var password = LgbCryptography.ComputeHash(input.Credential, user.PasswordSalt);
                ThrowIf(user.Password != password, new PasswordWrongException());
            }
            else
            {
                user = await _userRepository
                    .Where(a => a.Phone == input.Identifer)
                    .FirstAsync();
                ThrowIf(user == null, new AdminUserNotExistsException(input.Identifer));

                var cacheKey = SystemCacheKeyPrefixDefinition.LoginCaptcha + input.Identifer;
                var captcha = await Cache.GetAsync(cacheKey);
                ThrowIf(captcha.IsNullOrWhiteSpace(), new UnGetCaptchaException());
                ThrowIf(captcha != input.Credential, new CaptchaWrongException());
            }

            return Mapper.Map<AdminLoginDto>(user);
        }

        public async Task<AdminUserDto> GetUserAsync()
        {
            var user = await _userRepository.GetAsync(CurrentUser.Id);
            return Mapper.Map<AdminUserDto>(user);
        }

        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserMenuDto>> GetMenuAsync()
        {
            var permission = await GetPermissionAsync(CurrentUser.Id);
            if (permission.IsNullOrEmpty()) return null;

            var permissionIds = permission.Where(a => !a.ParentId.HasValue).Select(a => a.Id);

            var menus = await _menuRepository
                .Where(a => !a.PermissionId.HasValue || permissionIds.Contains(a.PermissionId.Value))
                .OrderBy(a => a.Sort)
                .ToTreeListAsync();

            var result = Mapper.Map<List<UserMenuDto>>(menus);

            return result;
        }

        /// <summary>
        /// 校验账号权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<bool> CheckPermissionAsync(int userId, string permission)
        {
            var permissions = await GetPermissionAsync(userId);
            var result = permissions?.Any(a => a.Name == permission) ?? false;
            return result;
        }

        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        private async Task<List<Permission>> GetPermissionAsync(int userId)
        {
            var cacheKey = SystemCacheKeyPrefixDefinition.UserPermission + userId;
            var userPermission = await Cache.GetAsync<List<Permission>>(cacheKey);
            if (userPermission == null)
            {
                var user = await _userRepository.Select
                    .IncludeMany(a => a.Roles)
                    .Where(a => a.Id == userId)
                    .FirstAsync();

                var roleIds = user.Roles.Select(b => b.Id).ToList();
                if (roleIds.IsNullOrEmpty()) return null;

                var roles = await _roleRepository.Select
                    .IncludeMany(a => a.Permissions)
                    .Where(a => roleIds.Contains(a.Id))
                    .ToListAsync();

                userPermission = roles.Select(a => a.Permissions)
                    .SelectMany(a => a).ToList();
                if (userPermission.IsNullOrEmpty()) return null;

                /*
                 * 用户权限校验伴随着每一次接口请求，并发高，IO频繁，存入缓存提升效率
                 * 在用户分配角色和角色分配权限时，更新缓存
                 */
                await Cache.SetAsync(cacheKey, userPermission);
            }

            return userPermission;
        }
    }
}