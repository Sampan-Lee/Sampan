using System;

namespace Sampan.Common.Extension
{
    /// <summary>
    /// Decimal 四舍五入函数
    /// </summary>
    public static class DecimalExtension
    {
        /// <summary>
        /// 四舍五入函数
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static Decimal ToRound(this Decimal value, int decimals)
        {
            return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
        }
    }
}