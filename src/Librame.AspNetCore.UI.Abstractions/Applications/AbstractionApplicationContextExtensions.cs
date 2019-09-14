#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Linq;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 抽象应用上下文静态扩展。
    /// </summary>
    public static class AbstractionApplicationContextExtensions
    {
        /// <summary>
        /// 获取 UI 信息（如果当前路由无区域信息，则返回可能加载的初始 UI 信息）。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="descriptor">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <returns>返回 <see cref="IUiInfo"/>。</returns>
        public static IUiInfo GetUiInfo(this IApplicationContext context, RouteDescriptor descriptor)
        {
            if (true == descriptor?.Area.IsNotNullOrEmpty())
                return context.Uis[descriptor.Area];

            return context.Uis?.Values?.FirstOrDefault();
        }

    }
}
