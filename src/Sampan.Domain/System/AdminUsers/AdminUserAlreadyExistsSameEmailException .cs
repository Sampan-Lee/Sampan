using Sampan.Common.Util;

namespace Sampan.Domain.System
{
    public class AdminUserAlreadyExistsSameEmailException : BusinessException
    {
        public AdminUserAlreadyExistsSameEmailException(string email)
            : base($"邮箱已存在：{email}")
        {
        }
    }
}