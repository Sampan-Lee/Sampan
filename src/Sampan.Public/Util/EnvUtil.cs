using System;
using System.Collections.Generic;
using System.Text;

namespace Sampan.Common.Util
{
    /// <summary>
    /// 程序环境帮助类
    /// </summary>
   public class EnvUtil
    {
        private static readonly string _envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        /// <summary>
        /// 判断是否是线上环境
        /// </summary>
        /// <returns></returns>
        public static bool IsOnline => _envName == EnvEnum.Production.ToString();

        /// <summary>
        /// 运行环境
        /// </summary>
        public enum EnvEnum
        {
            Development =0,
            Testing =1,
            Production =2,
        }
    }
}
