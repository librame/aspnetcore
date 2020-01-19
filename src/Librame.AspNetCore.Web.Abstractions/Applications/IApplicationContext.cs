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

namespace Librame.AspNetCore.Web.Applications
{
    using Extensions.Core.Services;
    using Projects;
    using Services;
    using Themepacks;

    /// <summary>
    /// 应用上下文接口。
    /// </summary>
    public interface IApplicationContext
    {
        /// <summary>
        /// 应用当事人。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IApplicationPrincipal"/>。
        /// </value>
        IApplicationPrincipal Principal { get; }

        /// <summary>
        /// 项目上下文。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IProjectContext"/>。
        /// </value>
        IProjectContext Project { get; }

        /// <summary>
        /// 主题包上下文。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IThemepackContext"/>。
        /// </value>
        IThemepackContext Themepack { get; }

        /// <summary>
        /// Web 主机环境。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IWebHostEnvironment"/>。
        /// </value>
        IWebHostEnvironment Environment { get; }

        /// <summary>
        /// 服务工厂。
        /// </summary>
        /// <value>
        /// 返回 <see cref="ServiceFactory"/>。
        /// </value>
        ServiceFactory ServiceFactory { get; }

        /// <summary>
        /// 版权声明服务。
        /// </summary>
        /// <value>
        /// 返回 <see cref="ICopyrightService"/>。
        /// </value>
        ICopyrightService Copyright { get; }


        /// <summary>
        /// 当前主题包信息。
        /// </summary>
        IThemepackInfo CurrentThemepackInfo { get; set; }

        /// <summary>
        /// 当前项目。
        /// </summary>
        (IProjectInfo Info, IProjectNavigation Navigation) CurrentProject { get; }


        /// <summary>
        /// 设置当前项目。
        /// </summary>
        /// <param name="area">给定的区域（请确保项目信息名称与给定的区域保持一致）。</param>
        /// <returns>返回包含 <see cref="IProjectInfo"/> 与 <see cref="IProjectNavigation"/> 的元组。</returns>
        (IProjectInfo Info, IProjectNavigation Navigation) SetCurrentProject(string area);
    }
}
