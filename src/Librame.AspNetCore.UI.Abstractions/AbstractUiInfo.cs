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
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Librame.AspNetCore.UI
{
    using Extensions.Core;

    /// <summary>
    /// 抽象 UI 信息。
    /// </summary>
    public abstract class AbstractUiInfo : AbstractApplicationInfo, IUiInfo
    {
        /// <summary>
        /// 添加本地化信息集合。
        /// </summary>
        /// <param name="localizers">给定的 <see cref="ConcurrentDictionary{String, IStringLocalizer}"/>。</param>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactoryDelegate"/>。</param>
        public abstract void AddLocalizers(ref ConcurrentDictionary<string, IStringLocalizer> localizers, ServiceFactoryDelegate serviceFactory);

        /// <summary>
        /// 添加导航信息集合。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactoryDelegate"/>。</param>
        public abstract void AddNavigations(ref ConcurrentDictionary<string, List<NavigationDescriptor>> navigations, ServiceFactoryDelegate serviceFactory);
    }
}
