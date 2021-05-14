namespace Sampan.Application.System
{
    public static class SystemCacheKeyPrefixDefinition
    {
        /// <summary>
        /// 短信验证码
        /// </summary>
        public const string LoginCaptcha = "admin_login_captcha_";

        /// <summary>
        /// 用户权限：后缀为UserId
        /// </summary>
        public const string UserPermission = "user_permission_";
    }
}