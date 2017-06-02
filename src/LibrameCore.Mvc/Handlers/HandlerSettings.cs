#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;

namespace LibrameStandard.Handlers
{
    using Utilities;

    /// <summary>
    /// 处理程序设置接口。
    /// </summary>
    public interface IHandlerSettings
    {
    }

    /// <summary>
    /// 处理程序设置。
    /// </summary>
    public class HandlerSettings : IHandlerSettings
    {
        /// <summary>
        /// 构造一个默认处理程序适配器设置。
        /// </summary>
        public HandlerSettings()
        {
        }
        /// <summary>
        /// 构造一个处理程序适配器设置。
        /// </summary>
        /// <param name="path">给定的路径字符串。</param>
        public HandlerSettings(string path)
            : this(new PathString(path))
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
