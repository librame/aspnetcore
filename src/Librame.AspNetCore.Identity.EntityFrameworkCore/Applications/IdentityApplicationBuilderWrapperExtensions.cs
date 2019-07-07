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
    /// 身份应用程序构建器封装静态扩展。
    /// </summary>
    public static class IdentityApplicationBuilderWrapperExtensions
    {
        /// <summary>
        /// 使用身份扩展。
        /// </summary>
        /// <param name="wrapper">给定的 <see cref="IApplicationBuilderWrapper"/>。</param>
        /// <returns>返回 <see cref="IApplicationBuilderWrapper"/>。</returns>
        public static IApplicationBuilderWrapper UseIdentity(this IApplicationBuilderWrapper wrapper)
        {
            wrapper.Builder.UseAuthentication();

            return wrapper;
        }

    }
}
