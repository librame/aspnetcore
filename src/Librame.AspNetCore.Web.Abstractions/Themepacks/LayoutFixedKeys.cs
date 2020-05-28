#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.Web.Themepacks
{
    /// <summary>
    /// 布局固定键名集合。
    /// </summary>
    public static class LayoutFixedKeys
    {
        private static List<string> _fixedKeys = InitializeFixedKeys();

        private static List<string> InitializeFixedKeys()
            => new List<string> { nameof(Common), nameof(Login), nameof(Manage) };


        /// <summary>
        /// 公共布局。
        /// </summary>
        public static string Common
            => _fixedKeys[0];

        /// <summary>
        /// 登陆布局。
        /// </summary>
        public static string Login
            => _fixedKeys[1];

        /// <summary>
        /// 管理布局。
        /// </summary>
        public static string Manage
            => _fixedKeys[2];


        /// <summary>
        /// 添加键名。
        /// </summary>
        /// <param name="key">给定的键名。</param>
        public static void Add(string key)
            => _fixedKeys.Add(key);

        /// <summary>
        /// 包含键名。
        /// </summary>
        /// <param name="key">给定的键名。</param>
        /// <returns>返回布尔值。</returns>
        public static bool Contains(string key)
            => _fixedKeys.Any(fk => fk.Equals(key, StringComparison.OrdinalIgnoreCase));
    }
}
