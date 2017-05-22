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
using Microsoft.Extensions.Logging;
using System;

namespace Librame
{
    /// <summary>
    /// Librame 构建器资源静态扩展。
    /// </summary>
    public static class LibrameBuilderResourceExtensions
    {
        /// <summary>
        /// 记录本地化定位器调试消息。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="stringFactory">给定的字符串定位器工厂方法。</param>
        public static void LogLocalizerDebug(this ILibrameBuilder builder,
            Func<IStringLocalizer<ILibrameBuilder>, string> stringFactory)
        {
            builder.LogLocalizer(stringFactory, (log, msg) => log.LogDebug(msg));
        }

        /// <summary>
        /// 记录本地化定位器错误消息。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="stringFactory">给定的字符串定位器工厂方法。</param>
        public static void LogLocalizerError(this ILibrameBuilder builder,
            Func<IStringLocalizer<ILibrameBuilder>, string> stringFactory)
        {
            builder.LogLocalizer(stringFactory, (log, msg) => log.LogError(msg));
        }

        /// <summary>
        /// 记录本地化定位器信息消息。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="stringFactory">给定的字符串定位器工厂方法。</param>
        public static void LogLocalizerInfo(this ILibrameBuilder builder,
            Func<IStringLocalizer<ILibrameBuilder>, string> stringFactory)
        {
            builder.LogLocalizer(stringFactory, (log, msg) => log.LogInformation(msg));
        }

        /// <summary>
        /// 记录本地化定位器跟踪消息。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="stringFactory">给定的字符串定位器工厂方法。</param>
        public static void LogLocalizerTrace(this ILibrameBuilder builder,
            Func<IStringLocalizer<ILibrameBuilder>, string> stringFactory)
        {
            builder.LogLocalizer(stringFactory, (log, msg) => log.LogTrace(msg));
        }

        /// <summary>
        /// 记录本地化定位器警告消息。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="stringFactory">给定的字符串定位器工厂方法。</param>
        public static void LogLocalizerWarn(this ILibrameBuilder builder,
            Func<IStringLocalizer<ILibrameBuilder>, string> stringFactory)
        {
            builder.LogLocalizer(stringFactory, (log, msg) => log.LogWarning(msg));
        }

        /// <summary>
        /// 记录本地化定位器消息。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="stringFactory">给定的字符串定位器工厂方法。</param>
        /// <param name="logAction">给定的记录动作。</param>
        public static void LogLocalizer(this ILibrameBuilder builder,
            Func<IStringLocalizer<ILibrameBuilder>, string> stringFactory,
            Action<ILogger<ILibrameBuilder>, string> logAction)
        {
            Utility.ExceptionUtil.NotNull(stringFactory, nameof(stringFactory));
            Utility.ExceptionUtil.NotNull(logAction, nameof(logAction));

            var message = stringFactory.Invoke(builder.Localizer);
            Utility.ExceptionUtil.NotEmpty(message, nameof(message));

            logAction.Invoke(builder.Logger, message);
        }

    }
}
