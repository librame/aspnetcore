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

namespace LibrameCore.Handlers
{
    using Utilities;

    /// <summary>
    /// 处理程序选项。
    /// </summary>
    public class HandlerOptions : ILibrameOptions
    {
        /// <summary>
        /// 构造一个默认处理程序选项。
        /// </summary>
        public HandlerOptions()
        {
        }
        /// <summary>
        /// 构造一个处理程序选项。
        /// </summary>
        /// <param name="path">给定的路径字符串。</param>
        public HandlerOptions(string path)
            : this(new PathString(path))
        {
        }
        /// <summary>
        /// 构造一个处理程序选项。
        /// </summary>
        /// <param name="path">给定的路径字符串。</param>
        public HandlerOptions(PathString path)
        {
            Path = path.NotNull(nameof(path));
        }


        /// <summary>
        /// 映射路径。
        /// </summary>
        public PathString Path { get; set; }

        ///// <summary>
        ///// 映射操作。
        ///// </summary>
        //public Action<IApplicationBuilder> Handle { get; set; }
    }
}
