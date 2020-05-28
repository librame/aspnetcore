#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.IdentityServer.Web.Projects
{
    using AspNetCore.Web.Projects;
    using Extensions.Core.Services;

    /// <summary>
    /// 身份服务器项目信息。
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    public class IdentityServerProjectInfo : AbstractProjectInfo
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityServerProjectInfo"/>。
        /// </summary>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactory"/>。</param>
        public IdentityServerProjectInfo(ServiceFactory serviceFactory)
            : base(serviceFactory)
        {
        }


        /// <summary>
        /// 名称。
        /// </summary>
        public override string Name
            => nameof(IdentityServer);

        /// <summary>
        /// 本地化定位器。
        /// </summary>
        public override IStringLocalizer Localizer
            => ServiceFactory.GetRequiredService<IStringLocalizer<IdentityServerProjectInfoResource>>();
    }
}
