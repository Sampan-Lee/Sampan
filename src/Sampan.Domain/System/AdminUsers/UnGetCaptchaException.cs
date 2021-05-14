using Sampan.Common.Util;

namespace Sampan.Domain.System
{
    public class UnGetCaptchaException : BusinessException
    {
        public UnGetCaptchaException() :
            base("请先获取短信验证码")
        {
        }
    }
}