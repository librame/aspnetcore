#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Hosting;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 应用程序后置配置选项接口。
    /// </summary>
    public interface IApplicationPostConfigureOptions
    {
        /// <summary>
        /// 主题包信息。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IThemepackInfo"/>。
        /// </value>
        IThemepackInfo Themepack { get; }

        /// <summary>
        /// 主机环境。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IHostingEnvironment"/>。
        /// </value>
        IHostingEnvironment Environment { get; }

        /// <summary>
        /// 区域名称。
        /// </summary>
        string AreaName { get; }
    }
}
