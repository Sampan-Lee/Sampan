using System.Collections.Generic;
using System.Linq;
using Sampan.Common.Util;

namespace Sampan.Common.Extension
{
    public static class EnumerableExtension
    {
        /// <summary>
        /// 字典取值
        /// </summary>
        public static bool StringEmpty(this Dictionary<string, string> dic, string key)
        {
            return dic.ContainsKey(key) && !string.IsNullOrEmpty(dic[key]);
        }

        /// <summary>
        /// 判断集合是否为空(true表示有数据)
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable?.Any() ?? true;
        }

        /// <summary>
        /// 拼装集合
        /// </summary>
        /// <param name="pkValues"></param>
        /// <returns></returns>
        public static IEnumerable<int> AddRange(params IEnumerable<int>[] pkValues)
        {
            List<int> list = new List<int>();

            if (!pkValues.IsNullOrEmpty())
            {
                foreach (var item in pkValues)
                {
                    if (!item.IsNullOrEmpty())
                    {
                        foreach (var _item in item)
                        {
                            if (_item != 0) list.Add(_item);
                        }
                    }
                }
            }

            return list.Distinct();
        }
    }
}