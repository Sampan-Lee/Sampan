using Sampan.Common.Util;

namespace Sampan.Domain.System
{
    public class PasswordWrongException : BusinessException
    {
        public PasswordWrongException()
            : base("密码错误")
        {
        }
    }
}