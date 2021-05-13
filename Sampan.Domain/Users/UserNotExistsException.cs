using Sampan.Common.Util;

namespace Sampan.Domain.Users
{
    public class UserNotExistsException : BusinessException
    {
        public UserNotExistsException(string phone)
            : base($"用户不存在：{phone}")
        {
        }
    }
}