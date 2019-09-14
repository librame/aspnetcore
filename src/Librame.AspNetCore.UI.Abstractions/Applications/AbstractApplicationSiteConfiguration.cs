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
    /// 抽象应用站点配置。
    /// </summary>
    public abstract class AbstractApplicationSiteConfiguration : IApplicationSiteConfiguration
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractApplicationSiteConfiguration"/>。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="area">指定的区域。</param>
        protected AbstractApplicationSiteConfiguration(IApplicationContext context, string area)
        {
            Context = context.NotNull(nameof(context));
            Area = area;
        }


        /// <summary>
        /// 应用上下文。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IApplicationContext"/>。
        /// </value>
        public IApplicationContext Context { get; }

        /// <summary>
        /// 区域。
        /// </summary>
        public string Area { get; }
    }
}
