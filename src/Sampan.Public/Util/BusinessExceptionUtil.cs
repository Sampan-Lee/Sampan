using System;

namespace Sampan.Common.Util
{
    public static class BusinessExceptionUtil
    {
        /// <summary>
        /// 条件为真的时候抛出异常
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="message"></param>
        public static void ThrowIf(bool conditions, BusinessException exception)
        {
            if (conditions)
                throw exception;
        }
    }

    /// <summary>
    /// 业务校验异常
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
        }
    }
}