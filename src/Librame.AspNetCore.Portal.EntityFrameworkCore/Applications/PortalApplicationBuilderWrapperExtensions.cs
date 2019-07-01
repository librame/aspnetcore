#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Portal
{
    /// <summary>
    /// 门户应用程序构建器封装静态扩展。
    /// </summary>
    public static class PortalApplicationBuilderWrpperExtensions
    {
        /// <summary>
        /// 使用门户扩展。
        /// </summary>
        /// <param name="wrapper">给定的 <see cref="IApplicationBuilderWrapper"/>。</param>
        /// <returns>返回 <see cref="IApplicationBuilderWrapper"/>。</returns>
        public static IApplicationBuilderWrapper UsePortal(this IApplicationBuilderWrapper wrapper)
        {
            //loader.Builder.UseAuthentication();

            return wrapper;
        }

    }
}
