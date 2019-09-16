#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.UI
{
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 抽象界面应用信息。
    /// </summary>
    public abstract class AbstractInterfaceInfo : AbstractApplicationInfo, IInterfaceInfo
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractInterfaceInfo"/>。
        /// </summary>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactoryDelegate"/>。</param>
        public AbstractInterfaceInfo(ServiceFactoryDelegate serviceFactory)
        {
            ServiceFactory = serviceFactory.NotNull(nameof(serviceFactory));
        }


        /// <summary>
        /// 服务工厂。
        /// </summary>
        protected ServiceFactoryDelegate ServiceFactory { get; }


        /// <summary>
        /// 站点地图。
        /// </summary>
        /// <value>返回 <see cref="IInterfaceSitemap"/>。</value>
        public IInterfaceSitemap Sitemap
            => ServiceFactory.GetRequiredService<IInterfaceSitemap>();
    }
}
