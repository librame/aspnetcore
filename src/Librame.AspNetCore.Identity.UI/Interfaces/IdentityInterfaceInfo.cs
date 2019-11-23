﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Localization;

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;
    using Extensions.Core;

    /// <summary>
    /// 身份界面信息。
    /// </summary>
    public class IdentityInterfaceInfo : AbstractInterfaceInfo
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityInterfaceInfo"/>。
        /// </summary>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactory"/>。</param>
        public IdentityInterfaceInfo(ServiceFactory serviceFactory)
            : base(serviceFactory)
        {
        }


        /// <summary>
        /// 名称。
        /// </summary>
        public override string Name
            => Localizer.GetString(nameof(Name));


        /// <summary>
        /// 本地化定位器。
        /// </summary>
        public override IStringLocalizer Localizer
            => ServiceFactory.GetRequiredService<IStringLocalizer<IdentityInterfaceInfoResource>>();
    }
}
