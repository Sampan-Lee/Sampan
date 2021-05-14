using Sampan.Common.Util;

namespace Sampan.Domain.Users
{
    public class UserAlreadyExistsException : BusinessException
    {
        public UserAlreadyExistsException(string name)
            : base($"用户已存在：{name}")
        {
        }
    }
}