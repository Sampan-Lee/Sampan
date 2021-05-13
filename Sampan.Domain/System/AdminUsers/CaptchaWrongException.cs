using Sampan.Common.Util;

namespace Sampan.Domain.System
{
    public class CaptchaWrongException : BusinessException
    {
        public CaptchaWrongException()
            : base("验证码错误")
        {
        }
    }
}