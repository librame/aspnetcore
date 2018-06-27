#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Abstractions;

namespace LibrameCore.Abstractions
{
    /// <summary>
    /// <see cref="ILibraryDependency"/> 静态扩展。
    /// </summary>
    public static class CoreLibraryDependencyExtensions
    {

        /// <summary>
        /// 添加核心库依赖。
        /// </summary>
        /// <param name="dependency">给定的 <see cref="ILibraryDependency"/>。</param>
        /// <param name="options">给定的 <see cref="CoreLibraryOptions"/>。</param>
        /// <returns>返回 <see cref="ILibraryDependency"/>。</returns>
        public static ILibraryDependency AddCoreLibrary(this ILibraryDependency dependency,
            CoreLibraryOptions options)
        {
            dependency.AddStandardLibrary(options);
            
            // 添加认证扩展
            dependency.Extensions.AddAuthenticationExtension(options.PostConfigureAuthentication);

            // 添加服务器扩展
            dependency.Extensions.AddServerExtension(options.PostConfigureServer);

            return dependency;
        }

    }
}
