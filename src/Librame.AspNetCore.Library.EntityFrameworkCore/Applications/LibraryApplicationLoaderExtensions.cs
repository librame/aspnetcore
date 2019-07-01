#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Library
{
    /// <summary>
    /// 文库应用程序加载器静态扩展。
    /// </summary>
    public static class LibraryApplicationLoaderExtensions
    {
        /// <summary>
        /// 使用文库扩展。
        /// </summary>
        /// <param name="loader">给定的 <see cref="IApplicationBuilderWrapper"/>。</param>
        /// <returns>返回 <see cref="IApplicationBuilderWrapper"/>。</returns>
        public static IApplicationBuilderWrapper UseLibrary(this IApplicationBuilderWrapper loader)
        {
            //loader.Builder.UseAuthentication();

            return loader;
        }

    }
}
