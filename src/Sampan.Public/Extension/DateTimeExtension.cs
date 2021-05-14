using System;

namespace Sampan.Common.Extension
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// 一年的第一天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1).Date;
        }

        /// <summary>
        /// 当前日期所在月份的第一天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return date.AddDays(-(date.Day) + 1).Date;
        }


        /// <summary>
        /// 取得某月的最后一天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(this DateTime date)
        {
            return date.AddDays(1 - date.Day).AddMonths(1).AddDays(-1).Date;
        }

        /// <summary>
        /// 与指定日期相差几个月
        /// </summary>
        /// <param name="dt">小的日期</param>
        /// <param name="date">大的日期</param>
        /// <returns></returns>
        public static int IntervalMonths(this DateTime dt, DateTime date)
        {
            return (date.Year * 12 - (12 - date.Month)) - (dt.Year * 12 - (12 - dt.Month));
        }

        public static DateTime ObjToDate(this object thisValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                reval = Convert.ToDateTime(thisValue);
            }

            return reval;
        }

        public static string ObjToString(this object thisValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return "";
        }

        public static bool ObjToBool(this object thisValue)
        {
            bool reval = false;
            if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }

            return reval;
        }

        /// <summary>
        /// 获取星期几
        /// </summary>
        /// <returns></returns>
        public static string GetWeek(DateTime dateTime = default)
        {
            if (dateTime == default) dateTime = DateTime.Now;
            string week;
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    week = "周一";
                    break;
                case DayOfWeek.Tuesday:
                    week = "周二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "周三";
                    break;
                case DayOfWeek.Thursday:
                    week = "周四";
                    break;
                case DayOfWeek.Friday:
                    week = "周五";
                    break;
                case DayOfWeek.Saturday:
                    week = "周六";
                    break;
                case DayOfWeek.Sunday:
                    week = "周日";
                    break;
                default:
                    week = "N/A";
                    break;
            }

            return week;
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static long UnixTimestamp(this DateTime dt)
        {
            var unixStartTime = new DateTime(1970, 1, 1, 8, 0, 0, 0);
            long timestamp =
                (dt.ToUniversalTime().Ticks - unixStartTime.ToUniversalTime().Ticks) / 10000000; //除10000调整为13位      
            //long t = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            return timestamp;
        }

        public static DateTime UnixDateTime(this DateTime dt)
        {
            return new DateTime(1970, 1, 1);
        }

        /// <summary>
        /// 获取这个日期的最后一秒
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime ToLastDateTime(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
        }
    }
}