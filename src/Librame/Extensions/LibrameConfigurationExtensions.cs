﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Utility;
using System;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// Librame 配置静态扩展。
    /// </summary>
    public static class LibrameConfigurationExtensions
    {
        /// <summary>
        /// 尝试获取指定键名的值。
        /// </summary>
        /// <remarks>
        /// 始终不会返回空或空字符串。
        /// </remarks>
        /// <param name="configuration">给定的配置接口。</param>
        /// <param name="key">给定的键名。</param>
        /// <param name="defaultValue">给定的默认值（如果当前取得的键值为空时有效）。</param>
        /// <returns>返回键值或默认值。</returns>
        public static string TryGetValue(this IConfiguration configuration, string key, string defaultValue)
        {
            var value = configuration[key];

            if (string.IsNullOrEmpty(value))
                return defaultValue.NotNullOrEmpty(nameof(defaultValue));

            return value;
        }

        /// <summary>
        /// 尝试获取指定键名的值（支持自定义转换值）。
        /// </summary>
        /// <param name="configuration">给定的配置接口。</param>
        /// <param name="key">给定的键名。</param>
        /// <param name="defaultValue">给定的默认值（如果当前取得的键值为空时有效）。</param>
        /// <param name="converter">给定的转换方法。</param>
        /// <returns>返回键值或默认值。</returns>
        public static TValue TryGetValue<TValue>(this IConfiguration configuration, string key, TValue defaultValue,
            Func<string, TValue> converter)
        {
            var value = configuration[key];

            if (string.IsNullOrEmpty(value))
                return defaultValue.NotNull(nameof(defaultValue));

            return converter.NotNull(nameof(converter)).Invoke(value);
        }

    }
}