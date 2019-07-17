#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Builder;

namespace Librame.AspNetCore.Identity
{
    /// <summary>
    /// 身份应用程序配置器静态扩展。
    /// </summary>
    public static class IdentityApplicationBuilderWrapperExtensions
    {
        /// <summary>
        /// 使用身份应用程序。
        /// </summary>
        /// <param name="configurator">给定的 <see cref="IApplicationConfigurator"/>。</param>
        /// <returns>返回 <see cref="IApplicationConfigurator"/>。</returns>
        public static IApplicationConfigurator UseIdentity(this IApplicationConfigurator configurator)
        {
            configurator.Builder.UseAuthentication();

            return configurator;
        }

    }
}
