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
    using Extensions;

    /// <summary>
    /// 抽象应用程序上下文。
    /// </summary>
    public abstract class AbstractApplicationContext : AbstractApplicationInfo, IApplicationContext
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractApplicationContext"/> 实例。
        /// </summary>
        /// <param name="localization">给定的 <see cref="IApplicationLocalization"/>。</param>
        /// <param name="navigation">给定的 <see cref="IApplicationNavigation"/>。</param>
        /// <param name="principal">给定的 <see cref="IApplicationPrincipal"/>。</param>
        /// <param name="environment">给定的 <see cref="IHostingEnvironment"/>。</param>
        /// <param name="info">给定的 <see cref="IThemepackInfo"/>。</param>
        public AbstractApplicationContext(IApplicationLocalization localization,
            IApplicationNavigation navigation,
            IApplicationPrincipal principal,
            IHostingEnvironment environment,
            IThemepackInfo info)
        {
            Localization = localization.NotNull(nameof(localization));
            Navigation = navigation.NotNull(nameof(navigation));
            Principal = principal.NotNull(nameof(principal));
            Environment = environment.NotNull(nameof(environment));
            Info = info.NotNull(nameof(info));
        }


        /// <summary>
        /// 本地化。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IApplicationLocalization"/>。
        /// </value>
        public IApplicationLocalization Localization { get; }

        /// <summary>
        /// 导航。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IApplicationNavigation"/>。
        /// </value>
        public IApplicationNavigation Navigation { get; }

        /// <summary>
        /// 当事人。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IApplicationPrincipal"/>。
        /// </value>
        public IApplicationPrincipal Principal { get; }

        /// <summary>
        /// 主机环境。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IHostingEnvironment"/>。
        /// </value>
        public IHostingEnvironment Environment { get; }

        /// <summary>
        /// 信息。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IThemepackInfo"/>。
        /// </value>
        public IThemepackInfo Info { get; }
    }
}
