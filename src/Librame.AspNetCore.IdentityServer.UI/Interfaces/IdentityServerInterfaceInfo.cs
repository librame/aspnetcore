#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Localization;

namespace Librame.AspNetCore.IdentityServer.UI
{
    using AspNetCore.UI;
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 身份服务器界面信息。
    /// </summary>
    public class IdentityServerInterfaceInfo : AbstractInterfaceInfo
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityServerInterfaceInfo"/>。
        /// </summary>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactoryDelegate"/>。</param>
        public IdentityServerInterfaceInfo(ServiceFactoryDelegate serviceFactory)
            : base(serviceFactory)
        {
        }


        /// <summary>
        /// 名称。
        /// </summary>
        public override string Name
            => Localizer[nameof(Name)];


        /// <summary>
        /// 本地化定位器。
        /// </summary>
        public override IStringLocalizer Localizer
            => ServiceFactory.NotNull(nameof(ServiceFactory)).GetRequiredService<IStringLocalizer<IdentityServerInterfaceInfoResource>>();
    }
}
