using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Sampan.Common.Util
{
    public static class ArgumentUtil
    {
        public static void AssertValuesNotNull<T>(params T[] values) where T : class
        {
            int num = 0;
            while (true)
            {
                if (num < values.Length)
                {
                    T val = values[num];
                    if (val == null)
                    {
                        break;
                    }
                    num++;
                    continue;
                }
                return;
            }
            throw new ArgumentNullException();
        }

        public static void AssertValuesNotNull<T>(string paramName, params T[] values) where T : class
        {
            int num = 0;
            while (true)
            {
                if (num < values.Length)
                {
                    T val = values[num];
                    if (val == null)
                    {
                        break;
                    }
                    num++;
                    continue;
                }
                return;
            }
            throw new ArgumentNullException("Argument name: " + paramName);
        }

        public static void AssertNotNull<T>(T value, string paramName) where T : class
        {
            AssertValuesNotNull<T>(paramName, value);
        }

        public static void AssertValuesNotNull<T>(params T?[] values) where T : struct
        {
            int num = 0;
            while (true)
            {
                if (num < values.Length)
                {
                    T? val = values[num];
                    if (!val.HasValue)
                    {
                        break;
                    }
                    num++;
                    continue;
                }
                return;
            }
            throw new ArgumentNullException();
        }

        public static void AssertNotNull<T>(T value) where T : class
        {
            AssertValuesNotNull<T>(value);
        }

        public static void AssertValuesNotNull<T>(string paramName, params T?[] values) where T : struct
        {
            int num = 0;
            while (true)
            {
                if (num < values.Length)
                {
                    T? val = values[num];
                    if (!val.HasValue)
                    {
                        break;
                    }
                    num++;
                    continue;
                }
                return;
            }
            throw new ArgumentNullException("Argument name: " + paramName);
        }

        public static void AssertNotNull<T>(T? value, string paramName) where T : struct
        {
            AssertValuesNotNull<T>(paramName, value);
        }

        public static void AssertNotNull<T>(T? value) where T : struct
        {
            AssertValuesNotNull<T>(value);
        }

        public static void AssertValuesNotEmpty(params string[] values)
        {
            int num = 0;
            while (true)
            {
                if (num < values.Length)
                {
                    string text = values[num];
                    if (text == null)
                    {
                        throw new ArgumentNullException();
                    }
                    if (text.Length == 0)
                    {
                        break;
                    }
                    num++;
                    continue;
                }
                return;
            }
            throw new ArgumentOutOfRangeException(string.Empty, "String is empty.");
        }

        public static void AssertNotEmpty(string value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
            if (value.Length == 0)
            {
                throw new ArgumentOutOfRangeException(paramName, "String is empty");
            }
        }

        public static void AssertNotEmpty(string value)
        {
            AssertValuesNotEmpty(value);
        }

        public static void AssertValuesPositive(params int[] values)
        {
            int num = 0;
            int num2;
            while (true)
            {
                if (num < values.Length)
                {
                    num2 = values[num];
                    if (num2 <= 0)
                    {
                        break;
                    }
                    num++;
                    continue;
                }
                return;
            }
            throw new ArgumentOutOfRangeException("Argument not positive. Value: (" + num2 + ").");
        }

        public static void AssertValuesPositive(string paramName, params int[] values)
        {
            int num = 0;
            int num2;
            while (true)
            {
                if (num < values.Length)
                {
                    num2 = values[num];
                    if (num2 <= 0)
                    {
                        break;
                    }
                    num++;
                    continue;
                }
                return;
            }
            throw new ArgumentOutOfRangeException("Argument not positive. Argument name: " + paramName + "; Value: " + num2 + ".");
        }

        public static void AssertPositive(int value, string paramName)
        {
            AssertValuesPositive(paramName, value);
        }

        public static void AssertPositive(int value)
        {
            AssertValuesPositive(value);
        }

        public static void AssertInRange(string paramName, int value, int rangeBegin, int rangeEnd)
        {
            if (value < rangeBegin || value > rangeEnd)
            {
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} must be within the range {rangeBegin} - {rangeEnd}.  The value given was {value}.");
            }
        }

        public static void AssertIsTrue(bool assertion, string message)
        {
            if (!assertion)
            {
                throw new ArgumentException(message);
            }
        }

        public static void AssertIsTrue(bool assertion, string messageTemplate, params object[] parameters)
        {
            if (!assertion)
            {
                throw new ArgumentException(string.Format(messageTemplate, parameters));
            }
        }

        public static void AssertIsFalse(bool assertion, string message)
        {
            AssertIsTrue(!assertion, message);
        }

        public static void AssertIsFalse(bool assertion, string messageTemplate, params object[] parameters)
        {
            AssertIsTrue(!assertion, messageTemplate, parameters);
        }

        public static void AssertIsRegex(string regex, params string[] values)
        {
            Regex regex2 = new Regex(regex);
            int num = 0;
            string text;
            while (true)
            {
                if (num < values.Length)
                {
                    text = values[num];
                    if (!regex2.IsMatch(text))
                    {
                        break;
                    }
                    num++;
                    continue;
                }
                return;
            }
            throw new ArgumentOutOfRangeException(text, string.Format("{0} must be accorded with the regex。The value given was {0}.", text));
        }

        public static void AssertIsGuid(params string[] values)
        {
            foreach (string text in values)
            {
                if (!Guid.TryParse(text,out Guid guid))
                {
                    throw new ArgumentOutOfRangeException(text, string.Format("{0}不是合法的GUID。The value given was {0}.", text));
                }
            }
        }

        public static void AssertIsGuidList(IEnumerable<string> values)
        {
            foreach (string value in values)
            {
                AssertIsGuid(value);
            }
        }

        public static void AssertType<T>(params object[] objs)
        {
            int num = 0;
            while (true)
            {
                if (num < objs.Length)
                {
                    object obj = objs[num];
                    if (!(obj is T))
                    {
                        break;
                    }
                    num++;
                    continue;
                }
                return;
            }
            throw new ArgumentOutOfRangeException($"the value isn't {typeof(T).Name} Type");
        }

        public static void AssertNotNullOrEmpty(params string[] para)
        {
            int num = 0;
            string text;
            while (true)
            {
                if (num < para.Length)
                {
                    text = para[num];
                    if (string.IsNullOrEmpty(text))
                    {
                        break;
                    }
                    num++;
                    continue;
                }
                return;
            }
            throw new ArgumentNullException(text, "the string value is null or empty");
        }
    }
}
