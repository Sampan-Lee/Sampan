using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sampan.Common.Util
{
    public static class AsyncUtil
    {
        /// <summary>
        /// 设置上线文内容 避免多线程读取不到变量
        /// </summary>
        public static void SetDefaultContext()
        {
            
        }

        public static Task Run(Action action)
        {
            return Task.Run(() =>
            {
                action.Invoke();
            });
        }

        public static Task Run<T>(Action<T> action, T param)
        {
            return Task.Run(() =>
            {
                action.Invoke(param);
            });
        }

        #region 超时后才进入异步

        public delegate TR TimeOutDelegate<in T, out TR>(T param);

        /// <summary>
        /// Execute a method with timeout check
        /// </summary>
        /// <typeparam name="T">Target method parameter type</typeparam>
        /// <typeparam name="TR">The result type of execution</typeparam>
        /// <param name="timeoutMethod">Target method</param>
        /// <param name="param">Target method parameter</param>
        /// <param name="result">The result of execution</param>
        /// <param name="timeout">Set timeout length</param>
        /// <returns>Is timeout</returns>
        public static Boolean Execute<T, TR>(
        TimeOutDelegate<T, TR> timeoutMethod, T param, out TR result, TimeSpan timeout)
        {
            var asyncResult = timeoutMethod.BeginInvoke(param, null, null);

            var waitResult = asyncResult.AsyncWaitHandle.WaitOne(timeout);
            if (waitResult)
            {
                result = timeoutMethod.EndInvoke(asyncResult);
            }
            else
            {
                result = default(TR);
            }

            return false;
        } 

        #endregion
    }
}
