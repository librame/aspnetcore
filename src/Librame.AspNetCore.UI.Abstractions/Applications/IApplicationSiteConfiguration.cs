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
    /// <summary>
    /// 应用站点配置接口。
    /// </summary>
    public interface IApplicationSiteConfiguration
    {
        /// <summary>
        /// 应用上下文。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IApplicationContext"/>。
        /// </value>
        IApplicationContext Context { get; }

        /// <summary>
        /// 区域。
        /// </summary>
        string Area { get; }
    }
}
