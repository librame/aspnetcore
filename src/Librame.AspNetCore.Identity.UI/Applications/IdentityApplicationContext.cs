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
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;
    using Extensions;

    /// <summary>
    /// 身份应用程序上下文。
    /// </summary>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    public class IdentityApplicationContext<TUser> : AbstractApplicationContext
        where TUser : class
    {
        private readonly IStringLocalizer<LayoutViewResource> _localizer;


        /// <summary>
        /// 构造一个 <see cref="IdentityApplicationContext{TUser}"/> 实例。
        /// </summary>
        /// <param name="localization">给定的 <see cref="IApplicationLocalization"/>。</param>
        /// <param name="navigation">给定的 <see cref="IApplicationNavigation"/>。</param>
        /// <param name="principal">给定的 <see cref="IApplicationPrincipal"/>。</param>
        /// <param name="environment">给定的 <see cref="IHostingEnvironment"/>。</param>
        /// <param name="info">给定的 <see cref="IThemepackInfo"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer{LayoutViewResource}"/>。</param>
        public IdentityApplicationContext(IApplicationLocalization localization,
            IApplicationNavigation navigation,
            IApplicationPrincipal principal,
            IHostingEnvironment environment,
            IThemepackInfo info,
            IStringLocalizer<LayoutViewResource> localizer)
            : base(localization, navigation, principal, environment, info)
        {
            _localizer = localizer.NotNull(nameof(localizer));

            InitializeApplication();
        }


        private void InitializeApplication()
        {
            // 增加管理布局本地化资源
            Localization.AddOrUpdateManageLayout(_localizer);

            // 绑定身份导航
            Navigation.BindIdentityNavigation(_localizer);

            // 绑定身份当事人
            Principal.BindIdentityPrincipal<TUser>();
        }


        /// <summary>
        /// 名称。
        /// </summary>
        public override string Name => "LibrameCore Identity";

        /// <summary>
        /// 标题。
        /// </summary>
        public override string Title => "LibrameCore 身份";

        /// <summary>
        /// 联系。
        /// </summary>
        public override string Contact => "https://github.com/librame/LibrameCore";

        /// <summary>
        /// 版权。
        /// </summary>
        public override string Copyright => "librame.net";

        /// <summary>
        /// 版本。
        /// </summary>
        public override string Version => AssemblyVersion.ToString();

        /// <summary>
        /// 程序集。
        /// </summary>
        /// <value>返回 <see cref="System.Reflection.Assembly"/>。</value>
        public override Assembly Assembly => GetType().Assembly;
    }
}
