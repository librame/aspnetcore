#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Web.Projects
{
    using Extensions;

    /// <summary>
    /// 项目描述符。
    /// </summary>
    public class ProjectDescriptor
    {
        /// <summary>
        /// 构造一个 <see cref="ProjectDescriptor"/>。
        /// </summary>
        /// <param name="info">给定的 <see cref="IProjectInfo"/>。</param>
        /// <param name="navigation">给定的 <see cref="IProjectNavigation"/>。</param>
        public ProjectDescriptor(IProjectInfo info, IProjectNavigation navigation)
        {
            Info = info.NotNull(nameof(info));
            Navigation = navigation.NotNull(nameof(navigation));
        }


        /// <summary>
        /// 项目信息。
        /// </summary>
        public IProjectInfo Info { get; }

        /// <summary>
        /// 项目导航。
        /// </summary>
        public IProjectNavigation Navigation { get; }

        /// <summary>
        /// 根项目导航。
        /// </summary>
        public IProjectNavigation RootNavigation
            => Navigation.RootNavigation;

        /// <summary>
        /// 身份项目导航。
        /// </summary>
        public IProjectNavigation IdentityNavigation
            => Navigation.IdentityNavigation;

    }
}
