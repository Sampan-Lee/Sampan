using System;
using System.Threading.Tasks;
using Sampan.Application.System;
using Sampan.Common.Extension;
using Sampan.Common.Util;
using Sampan.Domain.System;
using Sampan.Domain.Users;
using Sampan.Infrastructure.Repository;
using Sampan.Infrastructure.Sms;
using Sampan.Service.Contract.Account.UserAccounts;

namespace Sampan.Application.Authorizes.UserAuthorizes
{
    /// <summary>
    /// App用户授权认证
    /// </summary>
    public class UserAccountService : Service, IUserAccountService
    {
        private const string _userLoginCaptchaCacheKey = "user_login_captcha_";
        private readonly IRepository<User> _userRepository;
        private readonly UserManager _userManager;
        private readonly ISmsService _smsService;

        public UserAccountService(IRepository<User> userRepository,
            UserManager userManager,
            ISmsService smsService)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _smsService = smsService;
        }

        /// <summary>
        /// 发送登录验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<bool> SendCaptcha(string phone)
        {
            var cacheKey = _userLoginCaptchaCacheKey + phone;
            var exist = await Cache.ExistAsync(cacheKey);
            ThrowIf(exist, new BusinessException("验证码有效期5分钟，请勿重复发送"));

            var smsType = (SmsType) Appsettings.app("AppSettings", "Sms", "SmsType").ToInt();
            var captcha = smsType == SmsType.DevSms ? "666666" : new Random().Next(100000, 999999).ToString();
            await Cache.SetAsync(cacheKey, captcha, TimeSpan.FromMinutes(5));

            var send = await _smsService.SendAsync(phone, captcha);
            return send;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="input"></param>
        public async Task Register(RegisterDto input)
        {
            var user = await _userManager.CreateAsync(input.Phone, String.Empty);

            await _userRepository.InsertAsync(user);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<UserLoginDto> LoginAsync(LoginUserDto input)
        {
            var user = await _userRepository
                .Where(a => a.Phone == input.Phone)
                .ToOneAsync();
            ThrowIf(user == null, new UserNotExistsException(input.Phone));

            var cacheKey = _userLoginCaptchaCacheKey + input.Phone;
            var captcha = await Cache.GetAsync(cacheKey);
            ThrowIf(captcha.IsNullOrWhiteSpace(), new UnGetCaptchaException());
            ThrowIf(captcha != input.Phone, new CaptchaWrongException());
            return Mapper.Map<UserLoginDto>(user);
        }
    }
}