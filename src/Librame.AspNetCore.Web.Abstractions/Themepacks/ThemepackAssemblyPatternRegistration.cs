#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Librame.AspNetCore.Web.Themepacks
{
    /// <summary>
    /// 主题包程序集模式注册。
    /// </summary>
    public static class ThemepackAssemblyPatternRegistration
    {
        private static readonly ConcurrentBag<string> _patterns
            = new ConcurrentBag<string>();


        static ThemepackAssemblyPatternRegistration()
        {
            if (_patterns.IsEmpty)
            {
                Add($@"^{nameof(Librame)}.{nameof(AspNetCore)}.{nameof(Web)}.{nameof(Themepacks)}.(\w+)$");
            }
        }


        /// <summary>
        /// 所有模式集合。
        /// </summary>
        public static IReadOnlyCollection<string> All
            => _patterns;

        /// <summary>
        /// 已注册的模式数。
        /// </summary>
        public static int Count
            => _patterns.Count;


        /// <summary>
        /// 添加模式。
        /// </summary>
        /// <param name="pattern">给定的模式。</param>
        /// <returns>返回字符串。</returns>
        public static string Add(string pattern)
        {
            _patterns.Add(pattern);
            return pattern;
        }


        /// <summary>
        /// 清空所有模式。
        /// </summary>
        public static void Clear()
            => _patterns.Clear();
    }
}
