#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Text.RegularExpressions;

namespace Librame.Utility
{
    /// <summary>
    /// <see cref="string"/> 实用工具。
    /// </summary>
    public static class StringUtil
    {
        /// <summary>
        /// 空字符串。
        /// </summary>
        public const string EMPTY = "";

        /// <summary>
        /// 空格字符串。
        /// </summary>
        public const string SPACE = " ";

        /// <summary>
        /// 逗号字符串。
        /// </summary>
        public const string COMMA = ",";

        /// <summary>
        /// 分号字符串。
        /// </summary>
        public const string SEMICOLON = ";";

        /// <summary>
        /// 等号字符串。
        /// </summary>
        public const string EQUALITY = "=";


        #region Type Converter

        /// <summary>
        /// 字符串转换为布尔值。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回经过转换的值或默认值 False。</returns>
        public static bool AsBool(this string str)
        {
            return str.As(false);
        }
        /// <summary>
        /// 字符串转换为布尔值。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="defaultValue">如果字符串为空或转换失败要返回的默认值。</param>
        /// <returns>返回经过转换的值或默认值。</returns>
        public static bool As(this string str, bool defaultValue)
        {
            return str.As(defaultValue, s => bool.Parse(s));
        }

        /// <summary>
        /// 字符串转换为时间。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回经过转换的值或默认值 DateTime.MaxValue。</returns>
        public static DateTime AsDateTime(this string str)
        {
            return str.As(DateTime.MaxValue);
        }
        /// <summary>
        /// 字符串转换为时间。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="defaultValue">如果字符串为空或转换失败要返回的默认值。</param>
        /// <returns>返回经过转换的值或默认值。</returns>
        public static DateTime As(this string str, DateTime defaultValue)
        {
            return str.As(defaultValue, s => DateTime.Parse(s));
        }

        /// <summary>
        /// 字符串转换为单精度浮点数。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回经过转换的值或默认值 0F。</returns>
        public static float AsFloat(this string str)
        {
            return str.As(0F);
        }
        /// <summary>
        /// 字符串转换为单精度浮点数。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="defaultValue">如果字符串为空或转换失败要返回的默认值。</param>
        /// <returns>返回经过转换的值或默认值。</returns>
        public static float As(this string str, float defaultValue)
        {
            return str.As(defaultValue, s => float.Parse(s));
        }

        /// <summary>
        /// 字符串转换为双精度浮点数。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回经过转换的值或默认值 0D。</returns>
        public static double AsDouble(this string str)
        {
            return str.As(0D);
        }
        /// <summary>
        /// 字符串转换为双精度浮点数。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="defaultValue">如果字符串为空或转换失败要返回的默认值。</param>
        /// <returns>返回经过转换的值或默认值。</returns>
        public static double As(this string str, double defaultValue)
        {
            return str.As(defaultValue, s => double.Parse(s));
        }

        /// <summary>
        /// 字符串转换为 GUID。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回经过转换的值或默认值 Guid.Empty。</returns>
        public static Guid AsGuid(this string str)
        {
            return str.As(Guid.Empty);
        }
        /// <summary>
        /// 字符串转换为 GUID。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="defaultValue">如果字符串为空或转换失败要返回的默认值。</param>
        /// <returns>返回经过转换的值或默认值。</returns>
        public static Guid As(this string str, Guid defaultValue)
        {
            return str.As(defaultValue, s => Guid.Parse(s));
        }

        /// <summary>
        /// 字符串转换为整数。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回经过转换的值或默认值 0L。</returns>
        public static long AsLong(this string str)
        {
            return str.As(0L);
        }
        /// <summary>
        /// 字符串转换为整数。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="defaultValue">如果字符串为空或转换失败要返回的默认值。</param>
        /// <returns>返回经过转换的值或默认值。</returns>
        public static long As(this string str, long defaultValue)
        {
            return str.As(defaultValue, s => long.Parse(s));
        }

        /// <summary>
        /// 字符串转换为整数。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回经过转换的值或默认值 0。</returns>
        public static int AsInt(this string str)
        {
            return str.As(0);
        }
        /// <summary>
        /// 字符串转换为整数。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="defaultValue">如果字符串为空或转换失败要返回的默认值。</param>
        /// <returns>返回经过转换的值或默认值。</returns>
        public static int As(this string str, int defaultValue)
        {
            return str.As(defaultValue, s => int.Parse(s));
        }

        /// <summary>
        /// 转换为字符串（虚方法，不执行实际转换）。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回当前值或默认值 String.Empty。</returns>
        public static string As(this string str)
        {
            return str.As(string.Empty);
        }
        /// <summary>
        /// 转换为字符串（虚方法，不执行实际转换）。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="defaultValue">如果字符串为空要返回的默认值。</param>
        /// <returns>返回当前值或默认值。</returns>
        public static string As(this string str, string defaultValue)
        {
            return str.As(defaultValue, s => s);
        }

        /// <summary>
        /// 字符串通用转换。
        /// </summary>
        /// <typeparam name="TValue">指定的值类型。</typeparam>
        /// <param name="str">给定的字符串。</param>
        /// <param name="defaultValue">如果字符串为空或转换失败要返回的默认值。</param>
        /// <param name="factory">给定的转换方法。</param>
        /// <returns>返回经过转换的值或默认值。</returns>
        public static TValue As<TValue>(this string str, TValue defaultValue, Func<string, TValue> factory)
        {
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            try
            {
                return factory.Invoke(str);
            }
            catch //(Exception ex)
            {
                return defaultValue;
            }
        }

        #endregion


        #region Singular & Plural

        /// <summary>
        /// 复数单词单数化。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// plural 为空或空字符串。
        /// </exception>
        /// <param name="plural">给定的复数化英文单词。</param>
        /// <returns>返回字符串。</returns>
        public static string AsSingularize(this string plural)
        {
            plural.NotEmpty(nameof(plural));

            Regex plural1 = new Regex("(?<keep>[^aeiou])ies$");
            Regex plural2 = new Regex("(?<keep>[aeiou]y)s$");
            Regex plural3 = new Regex("(?<keep>[sxzh])es$");
            Regex plural4 = new Regex("(?<keep>[^sxzhyu])s$");

            if (plural1.IsMatch(plural))
                return plural1.Replace(plural, "${keep}y");
            else if (plural2.IsMatch(plural))
                return plural2.Replace(plural, "${keep}");
            else if (plural3.IsMatch(plural))
                return plural3.Replace(plural, "${keep}");
            else if (plural4.IsMatch(plural))
                return plural4.Replace(plural, "${keep}");

            return plural;
        }


        /// <summary>
        /// 单数单词复数化。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// singular 为空或空字符串。
        /// </exception>
        /// <param name="singular">给定的单数化英文单词。</param>
        /// <returns>返回字符串。</returns>
        public static string AsPluralize(this string singular)
        {
            singular.NotEmpty(nameof(singular));

            Regex plural1 = new Regex("(?<keep>[^aeiou])y$");
            Regex plural2 = new Regex("(?<keep>[aeiou]y)$");
            Regex plural3 = new Regex("(?<keep>[sxzh])$");
            Regex plural4 = new Regex("(?<keep>[^sxzhy])$");

            if (plural1.IsMatch(singular))
                return plural1.Replace(singular, "${keep}ies");
            else if (plural2.IsMatch(singular))
                return plural2.Replace(singular, "${keep}s");
            else if (plural3.IsMatch(singular))
                return plural3.Replace(singular, "${keep}es");
            else if (plural4.IsMatch(singular))
                return plural4.Replace(singular, "${keep}s");

            return singular;
        }

        #endregion


        #region Naming Conventions

        /// <summary>
        /// 包含一到多个单词，每一个单词第一个字母大写，其余字母均小写。例如：HelloWorld 等。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// words 为空或空字符串。
        /// </exception>
        /// <param name="words">给定的英文单词（多个单词以空格区分）。</param>
        /// <returns>返回字符串。</returns>
        public static string AsPascalCasing(this string[] words)
        {
            words.NotNull(nameof(words));

            string str = string.Empty;

            foreach (var w in words)
            {
                // 首字母大写，其余字母均小写
                str += char.ToUpper(w[0]) + w.Substring(1).ToLower();
            }

            return str;
        }


        /// <summary>
        /// 包含一到多个单词，第一个单词小写，其余单词中每一个单词第一个字母大写，其余字母均小写。例如：helloWorld 等。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// words 为空或空字符串。
        /// </exception>
        /// <param name="words">给定的英文单词（多个单词以空格区分）。</param>
        /// <returns>返回字符串。</returns>
        public static string AsCamelCasing(this string[] words)
        {
            words.NotNull(nameof(words));

            // 首单词小写
            string str = words[0].ToLower();

            if (words.Length > 1)
            {
                for (var i = 1; i < words.Length; i++)
                {
                    var w = words[i];

                    // 首字母大写，其余字母均小写
                    str += char.ToUpper(w[0]) + w.Substring(1).ToLower();
                }
            }

            return str;
        }

        #endregion

    }
}
