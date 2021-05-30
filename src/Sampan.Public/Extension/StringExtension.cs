using System;
using System.Runtime.InteropServices;
using Sampan.Common.Util;

namespace Sampan.Common.Extension
{
    public static class StringExtension
    {
        private static bool _windows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static string ReplacePath(this string path)
        {
            if (string.IsNullOrEmpty(path))
                return "";
            if (_windows)
                return path.Replace("/", "\\");
            return path.Replace("\\", "/");
        }

        #region string

        /// <summary>
        /// 首字母大写/适用于前端小驼峰方式传参和C#大驼峰属性名对比
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToInitialUpper(this string str)
        {
            return str?.Substring(0, 1).ToUpper() + str?.Substring(1);
        }

        /// <summary>
        /// 判断字段不为空
        /// [null,""," "]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 判断字段不为空
        /// [null,""]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        #endregion

        /// <summary>
        /// 添加文件访问地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AddBasePath(this string str)
        {
            if (str.IsNullOrWhiteSpace()) return str;
            return $"{Appsettings.app("AppSettings", "Upload", "LinkAddress")}{str}";
        }

        /// <summary>
        /// 返回guid 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AddGuid(this string str)
        {
            return str.IsNullOrWhiteSpace() ? Guid.NewGuid().ToString() : str;
        }

        /// <summary>
        /// StringFormat
        /// </summary>
        /// <param name="str"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string Format(this string str, params string[] items)
        {
            return string.Format(str, items);
        }

        /// <summary>
        /// 汉子转拼音 首字母全称
        /// </summary>
        /// <param name="chinese"></param>
        /// <returns></returns>
        public static ChineseModel ToChinese(this string chinese)
        {
            return PingYinHelper.GetFirstSpell(chinese);
        }
    }
}