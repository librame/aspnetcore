#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Hosting;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 应用程序上下文接口。
    /// </summary>
    public interface IApplicationContext : IApplicationInfo
    {
        /// <summary>
        /// 本地化。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IApplicationLocalization"/>。
        /// </value>
        IApplicationLocalization Localization { get; }

        /// <summary>
        /// 导航。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IApplicationNavigation"/>。
        /// </value>
        IApplicationNavigation Navigation { get; }

        /// <summary>
        /// 当事人。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IApplicationPrincipal"/>。
        /// </value>
        IApplicationPrincipal Principal { get; }

        /// <summary>
        /// 主机环境。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IHostingEnvironment"/>。
        /// </value>
        IHostingEnvironment Environment { get; }

        /// <summary>
        /// 信息。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IThemepackInfo"/>。
        /// </value>
        IThemepackInfo Info { get; }
    }
}
