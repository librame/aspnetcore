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

    /// <summary>
    /// 抽象应用站点。
    /// </summary>
    public abstract class AbstractApplicationSite : IApplicationSite
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractApplicationSite"/>。
        /// </summary>
        /// <param name="navigation">给定的 <see cref="IApplicationSiteNavigation"/>。</param>
        protected AbstractApplicationSite(IApplicationSiteNavigation navigation)
        {
            Navs = navigation.NotNull(nameof(navigation));
        }


        /// <summary>
        /// 站点导航。
        /// </summary>
        public IApplicationSiteNavigation Navs { get; }
    }
}
