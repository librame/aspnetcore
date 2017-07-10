#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Utilities;
using Microsoft.AspNetCore.Http;

namespace LibrameCore.Handlers
{
    /// <summary>
    /// 处理程序设置。
    /// </summary>
    public class HandlerSettings : IHandlerSettings
    {
        /// <summary>
        /// 路径前缀。
        /// </summary>
        public const string PATH_PREFIX = "/handler";


        /// <summary>
        /// 构造一个默认处理程序适配器设置。
        /// </summary>
        public HandlerSettings()
        {
        }
        /// <summary>
        /// 构造一个处理程序适配器设置。
        /// </summary>
        /// <param name="appendPath">给定用于附加到前缀 <see cref="PATH_PREFIX"/> 的路径字符串。</param>
        public HandlerSettings(string appendPath)
            : this(new PathString(PATH_PREFIX + appendPath))
        {
        }
        /// <summary>
        /// 构造一个处理程序适配器设置。
        /// </summary>
        /// <param name="path">给定的路径字符串。</param>
        public HandlerSettings(PathString path)
        {
            Path = path.NotNull(nameof(path));
        }


        /// <summary>
        /// 映射路径。
        /// </summary>
        public PathString Path { get; set; }
    }
}
