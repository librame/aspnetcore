#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Librame.AspNetCore.Identity.Builders
{
    using AspNetCore.Web.Builders;
    using AspNetCore.Web.Descriptors;
    using AspNetCore.Web.Projects;
    using Extensions.Core.Builders;

    /// <summary>
    /// 身份 Web 构建器依赖。
    /// </summary>
    public class IdentityWebBuilderDependency : WebBuilderDependency
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityWebBuilderDependency"/>。
        /// </summary>
        /// <param name="parentDependency">给定的父级 <see cref="IExtensionBuilderDependency"/>（可选）。</param>
        public IdentityWebBuilderDependency(IExtensionBuilderDependency parentDependency = null)
            : base(parentDependency)
        {
        }


        /// <summary>
        /// 登入成功回调路径（默认身份导航管理）。
        /// </summary>
        public Func<IProjectNavigation, NavigationDescriptor> LoginSuccessfulCallbackNavigation { get; set; }
            = navs => navs.IdentityNavigation?.Manage;

        /// <summary>
        /// 登出成功回调路径（默认根导航首页）。
        /// </summary>
        public Func<IProjectNavigation, NavigationDescriptor> LogoutSuccessfulCallbackNavigation { get; set; }
            = navs => navs.RootNavigation.Index;

        /// <summary>
        /// 注册成功回调路径（默认根导航首页）。
        /// </summary>
        public Func<IProjectNavigation, NavigationDescriptor> RegisterSuccessfulCallbackNavigation { get; set; }
            = navs => navs.RootNavigation.Index;

        /// <summary>
        /// 双因子恢复码长度。
        /// </summary>
        public int TwoFactorRecoveryCodeLength { get; set; }
            = 5;
    }
}
