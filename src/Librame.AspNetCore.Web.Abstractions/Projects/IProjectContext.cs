#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;

namespace Librame.AspNetCore.Web.Projects
{
    using Extensions.Core.Services;

    /// <summary>
    /// 项目上下文接口。
    /// </summary>
    public interface IProjectContext
    {
        /// <summary>
        /// 服务工厂。
        /// </summary>
        ServiceFactory ServiceFactory { get; }

        /// <summary>
        /// 导航集合。
        /// </summary>
        IEnumerable<IProjectNavigation> Navigations { get; }

        /// <summary>
        /// 信息字典集合。
        /// </summary>
        IReadOnlyDictionary<string, IProjectInfo> Infos { get; }

        /// <summary>
        /// 登陆栏项目（根据配置的登陆栏项目名称查找对应的项目，如果不存在则使用当前项目）。
        /// </summary>
        (IProjectInfo Info, IProjectNavigation Navigation) Loginbar { get; }

        /// <summary>
        /// 当前项目。
        /// </summary>
        (IProjectInfo Info, IProjectNavigation Navigation) Current { get; set; }


        /// <summary>
        /// 设置当前项目。
        /// </summary>
        /// <param name="area">给定的区域（请确保项目信息名称与给定的区域保持一致）。</param>
        /// <returns>返回包含 <see cref="IProjectInfo"/> 与 <see cref="IProjectNavigation"/> 的元组。</returns>
        (IProjectInfo Info, IProjectNavigation Navigation) SetCurrent(string area);


        /// <summary>
        /// 查找指定名称的项目信息。
        /// </summary>
        /// <param name="name">给定的项目名称。</param>
        /// <returns>返回 <see cref="IProjectInfo"/>。</returns>
        IProjectInfo FindInfo(string name);

        /// <summary>
        /// 查找指定区域的项目导航。
        /// </summary>
        /// <param name="area">给定的区域。</param>
        /// <returns>返回 <see cref="IProjectNavigation"/>。</returns>
        IProjectNavigation FindNavigation(string area);
    }
}
