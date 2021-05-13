using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sampan.Common.Extension
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            // Get enum type and name
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                // If it's not defined as a field then return the numeric value
                return Convert.ToInt32(value).ToString();
            }
            // Get DescriptionAttribute, return the description in attribute if exist
            var attribute = value.GetAttribute<DescriptionAttribute>();
            if (attribute != null)
            {
                return attribute.Description;
            }
            // Return default name
            return name;
        }

        public static T GetAttribute<T>(this Enum value) where T : System.Attribute
        {
            Type type = value.GetType();
            var field = type.GetField(Enum.GetName(type, value)!);
            return (T)field.GetCustomAttributes(typeof(T),false).FirstOrDefault();
        }

        /// <summary>
        /// 获取枚举key 值
        /// </summary>
        public static int Key(this Enum value)
        {
            return Convert.ToInt32(value);
        }

        public static string StrValue(this System.Enum value)
        {
            return value.Key().ToString();
        }

        public static bool ValueEqualTo(this System.Enum @enum, string value)
        {
            return @enum.StrValue() == value;
        }
    }
}
