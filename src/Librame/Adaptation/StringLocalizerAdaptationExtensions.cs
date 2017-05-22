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

namespace Librame.Adaptation
{
    using Resources;

    /// <summary>
    /// 字符串定位器适配静态扩展。
    /// </summary>
    public static class StringLocalizerAdaptationExtensions
    {
        /// <summary>
        /// 通过程序集添加适配器模块集合（参考：Try adding adapter modules by {0} assembly）。
        /// </summary>
        /// <param name="localizer">给定的字符串定位器。</param>
        /// <param name="args">用于格式化的参数数组。</param>
        /// <returns>返回本地化字符串。</returns>
        public static string AsLocalizerAddingAdapterByAssembly(this IStringLocalizer<ILibrameBuilder> localizer,
            params object[] args)
        {
            return localizer.AsLocalizerString("AddingAdapterByAssembly",
                "Try adding adapter modules by {0} assembly", args);
        }

        /// <summary>
        /// 添加适配器模块（参考：Try adding {0}, {1} adapter module）。
        /// </summary>
        /// <param name="localizer">给定的字符串定位器。</param>
        /// <param name="args">用于格式化的参数数组。</param>
        /// <returns>返回本地化字符串。</returns>
        public static string AsLocalizerAddingAdapter(this IStringLocalizer<ILibrameBuilder> localizer,
            params object[] args)
        {
            return localizer.AsLocalizerString("AddingAdapter",
                "Try adding {0}, {1} adapter module", args);
        }

    }
}
