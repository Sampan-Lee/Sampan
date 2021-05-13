using Sampan.Common.Util;

namespace Sampan.Domain.System
{
    public class AdminUserNotExistsException : BusinessException
    {
        public AdminUserNotExistsException(string identification)
            : base($"用户不存在：{identification}")
        {
        }
    }
}