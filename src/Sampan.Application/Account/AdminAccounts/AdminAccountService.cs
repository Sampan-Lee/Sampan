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
using Sampan.Service.Contract.System.Permissions;
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
        private readonly IRepository<Menu> _menuRepository;
        private readonly IAdminUserService _userService;
        private readonly IPermissionService _permissionService;
        private readonly ISmsService _smsService;

        public AdminAccountService(IRepository<AdminUser> userRepository,
            IRepository<Menu> menuRepository,
            IAdminUserService userService,
            IPermissionService permissionService,
            ISmsService smsService)
        {
            _userRepository = userRepository;
            _permissionService = permissionService;
            _menuRepository = menuRepository;
            _userService = userService;
            _smsService = smsService;
        }

        /// <summary>
        /// 发送登录验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<bool> SendCaptchaAsync(string phone)
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

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<AdminUserDto> GetUserAsync()
        {
            return await _userService.GetAsync(CurrentUser.Id);
        }

        /// <summary>
        /// 获取用户菜单,只支持三级
        /// </summary>
        /// <returns></returns>
        public async Task<List<AdminMenuDto>> GetMenuAsync()
        {
            var permission = await _permissionService.GetAsync(CurrentUser.Id);
            if (permission.IsNullOrEmpty()) return null;

            var permissionIds = permission.Where(a => !a.ParentId.HasValue).Select(a => a.Id).ToList();

            var menus = await _menuRepository
                .Where(a => permissionIds.Contains(a.PermissionId.Value)
                            || (!a.PermissionId.HasValue && a.Id == 1)
                            || a.Children.AsSelect().Any(b => permissionIds.Contains(b.PermissionId.Value))
                            || a.Children.AsSelect().Any(c =>
                                c.Children.AsSelect().Any(d => permissionIds.Contains(d.PermissionId.Value)))
                )
                .OrderBy(a => a.Sort)
                .ToTreeListAsync();

            var result = Mapper.Map<List<AdminMenuDto>>(menus);

            return result;
        }

        /// <summary>
        /// 获取用户操作权限
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetPermissionAsync()
        {
            var permission = await _permissionService.GetAsync(CurrentUser.Id);
            return permission.Select(a => a.Name).ToList();
        }

        /// <summary>
        /// 校验账号权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<bool> CheckPermissionAsync(int userId, string permission)
        {
            var permissions = await _permissionService.GetAsync(userId);
            var result = permissions?.Any(a => a.Name == permission) ?? false;
            return result;
        }
    }
}