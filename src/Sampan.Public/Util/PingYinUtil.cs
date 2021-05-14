using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Text;

namespace Sampan.Common.Util
{
    public class PingYinHelper
    {
        private static Encoding gb2312 = Encoding.GetEncoding("GB2312");

        /// <summary>
        /// 汉字转全拼
        /// </summary>
        /// <param name="strChinese"></param>
        /// <returns></returns>
        public static string ConvertToAllSpell(string strChinese)
        {
            try
            {
                if (strChinese.Length != 0)
                {
                    StringBuilder fullSpell = new StringBuilder();
                    for (int i = 0; i < strChinese.Length; i++)
                    {
                        var chr = strChinese[i];
                        fullSpell.Append(GetSpell(chr));
                    }

                    return fullSpell.ToString().ToUpper();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("全拼转化出错！" + e.Message);
            }

            return string.Empty;
        }

        /// <summary>
        /// 汉字转首字母
        /// </summary>
        /// <param name="strChinese"></param>
        /// <returns></returns>
        public static ChineseModel GetFirstSpell(string strChinese)
        {
            //NPinyin.Pinyin.GetInitials(strChinese)  有Bug  洺无法识别
            //return NPinyin.Pinyin.GetInitials(strChinese);
            ChineseModel modelPinyin = new ChineseModel();
            try
            {
                if (strChinese.Length != 0)
                {
                    StringBuilder fullSpell = new StringBuilder();
                    for (int i = 0; i < strChinese.Length; i++)
                    {
                        var chr = strChinese[i];
                        fullSpell.Append(GetSpell(chr)[0]);
                    }
                    modelPinyin.Chinese = fullSpell.ToString().ToUpper();
                    modelPinyin.ChineseTag = modelPinyin.Chinese.Substring(0,1);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("首字母转化出错！" + e.Message);
            }

            return modelPinyin;
        }

        private static string GetSpell(char chr)
        {
            var coverchr = NPinyin.Pinyin.GetPinyin(chr);

            bool isChineses = ChineseChar.IsValidChar(coverchr[0]);
            if (isChineses)
            {
                ChineseChar chineseChar = new ChineseChar(coverchr[0]);
                foreach (string value in chineseChar.Pinyins)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        return value.Remove(value.Length - 1, 1);
                    }
                }
            }

            return coverchr;

        }
    }

    /// <summary>
    /// 拼音转化实体
    /// </summary>
    public class ChineseModel
    {
        /// <summary>
        /// 拼音
        /// </summary>
        public string Chinese { get; set; }

        /// <summary>
        /// 拼音标签
        /// </summary>
        public string ChineseTag { get; set; }
    }
}
