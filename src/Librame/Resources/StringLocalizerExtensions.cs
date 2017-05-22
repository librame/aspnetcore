#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Localization;

namespace Librame.Resources
{
    /// <summary>
    /// 字符串定位器静态扩展。
    /// </summary>
    public static class StringLocalizerExtensions
    {
        /// <summary>
        /// 转换为定位器字符串。
        /// </summary>
        /// <param name="localizer">给定的字符串定位器。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="defaultValue">给定的默认值。</param>
        /// <param name="args">用于格式化的参数数组。</param>
        /// <returns>返回本地化字符串。</returns>
        public static string AsLocalizerString(this IStringLocalizer<ILibrameBuilder> localizer,
            string name, string defaultValue, params object[] args)
        {
            var pair = localizer[name];
            if (pair == null)
                return string.Format(defaultValue, args);

            return string.Format(pair.Value, args);
        }

    }
}
