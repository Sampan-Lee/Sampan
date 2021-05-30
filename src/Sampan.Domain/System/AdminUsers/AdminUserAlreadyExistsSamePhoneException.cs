using Sampan.Common.Util;

namespace Sampan.Domain.System
{
    public class AdminUserAlreadyExistsSamePhoneException : BusinessException
    {
        public AdminUserAlreadyExistsSamePhoneException(string phone)
            : base($"手机号已存在：{phone}")
        {
        }
    }
}