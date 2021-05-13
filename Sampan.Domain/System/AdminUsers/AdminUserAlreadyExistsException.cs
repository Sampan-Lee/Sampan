using Sampan.Common.Util;

namespace Sampan.Domain.System
{
    public class AdminUserAlreadyExistsException : BusinessException
    {
        public AdminUserAlreadyExistsException(string name)
            : base($"用户已存在：{name}")
        {
        }
    }
}