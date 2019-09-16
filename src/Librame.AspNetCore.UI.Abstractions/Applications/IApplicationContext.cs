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
using System.Collections.Concurrent;

namespace Librame.AspNetCore.UI
{
    using Extensions.Core;

    /// <summary>
    /// 应用上下文接口。
    /// </summary>
    public interface IApplicationContext
    {
        /// <summary>
        /// 服务工厂。
        /// </summary>
        /// <value>
        /// 返回 <see cref="ServiceFactoryDelegate"/>。
        /// </value>
        ServiceFactoryDelegate ServiceFactory { get; }
        

        /// <summary>
        /// 应用当事人。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IApplicationPrincipal"/>。
        /// </value>
        IApplicationPrincipal Principal { get; }

        /// <summary>
        /// 主机环境。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IHostingEnvironment"/>。
        /// </value>
        IHostingEnvironment Environment { get; }


        /// <summary>
        /// 界面信息集合。
        /// </summary>
        ConcurrentDictionary<string, IInterfaceInfo> InterfaceInfos { get; }

        /// <summary>
        /// 主题包信息集合。
        /// </summary>
        ConcurrentDictionary<string, IThemepackInfo> ThemepackInfos { get; }


        /// <summary>
        /// 当前界面信息。
        /// </summary>
        IInterfaceInfo CurrentInterfaceInfo { get; }

        /// <summary>
        /// 当前主题包信息。
        /// </summary>
        IThemepackInfo CurrentThemepackInfo { get; }


        /// <summary>
        /// 获取界面信息。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="orDefault">如果不存在指定名称的界面信息，则返回已加载集合的默认界面信息。</param>
        /// <returns>返回 <see cref="IInterfaceInfo"/>。</returns>
        IInterfaceInfo GetInterfaceInfo(string name, bool orDefault = true);

        /// <summary>
        /// 设置当前界面信息。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="orDefault">如果不存在指定名称的界面信息，则采用已加载集合的默认界面信息。</param>
        /// <returns>返回 <see cref="IInterfaceInfo"/>。</returns>
        IInterfaceInfo SetCurrentInterfaceInfo(string name, bool orDefault = true);


        /// <summary>
        /// 获取主题包信息。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="orDefault">如果不存在指定名称的界面信息，则返回已加载集合的默认界面信息。</param>
        /// <returns>返回 <see cref="IThemepackInfo"/>。</returns>
        IThemepackInfo GetThemepackInfo(string name, bool orDefault = true);

        /// <summary>
        /// 设置当前主题包信息。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="orDefault">如果不存在指定名称的主题包信息，则采用已加载集合的默认主题包信息。</param>
        /// <returns>返回 <see cref="IThemepackInfo"/>。</returns>
        IThemepackInfo SetCurrentThemepackInfo(string name, bool orDefault = true);
    }
}
