#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Librame.AspNetCore.Web.Projects
{
    using Applications;
    using Builders;
    using Extensions;
    using Extensions.Core.Services;

    /// <summary>
    /// 项目配置基类。
    /// </summary>
    public class ProjectConfigurationBase : AbstractProjectConfiguration,
        IPostConfigureOptions<CookieAuthenticationOptions>,
        IPostConfigureOptions<StaticFileOptions>,
        IPostConfigureOptions<RazorViewEngineOptions>
    {
        /// <summary>
        /// 构造一个 <see cref="ProjectConfigurationBase"/>。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="area">给定的区域。</param>
        protected ProjectConfigurationBase(IApplicationContext context, string area)
            : base(context)
        {
            Area = area.NotEmpty(nameof(area));
            Navigation = Context.SetCurrentProject(area).Navigation;
        }


        /// <summary>
        /// 区域。
        /// </summary>
        protected string Area { get; }

        /// <summary>
        /// 导航。
        /// </summary>
        protected IProjectNavigation Navigation { get; }


        /// <summary>
        /// 构建器。
        /// </summary>
        protected IWebBuilder Builder
            => Context.Themepack.Builder;

        /// <summary>
        /// 构建器选项。
        /// </summary>
        protected WebBuilderOptions BuilderOptions
            => Context.ServiceFactory.GetRequiredService<IOptions<WebBuilderOptions>>().Value;


        /// <summary>
        /// 后置配置。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="options">给定的 <see cref="CookieAuthenticationOptions"/>。</param>
        public virtual void PostConfigure(string name, CookieAuthenticationOptions options)
        {
            options = options ?? new CookieAuthenticationOptions();

            if (IdentityConstants.ApplicationScheme.Equals(name, StringComparison.Ordinal))
            {
                options.LoginPath = Navigation.Login.Route.ToString();
                options.LogoutPath = Navigation.Logout.Route.ToString();
                options.AccessDeniedPath = Navigation.AccessDenied.Route.ToString();
            }
        }


        /// <summary>
        /// 后置配置。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="options">给定的 <see cref="RazorViewEngineOptions"/>。</param>
        public virtual void PostConfigure(string name, RazorViewEngineOptions options)
        {
            options = options ?? new RazorViewEngineOptions();

            var expander = options.ViewLocationExpanders?.FirstOrDefault(p => p is LanguageViewLocationExpander);
            if (expander != null)
                options.ViewLocationExpanders.Remove(expander);

            options.ViewLocationExpanders.Add(new ResetLanguageViewLocationExpander());
        }


        /// <summary>
        /// 后置配置。
        /// </summary>
        /// <param name="name">给定的配置名称。</param>
        /// <param name="options">给定的 <see cref="StaticFileOptions"/>。</param>
        public virtual void PostConfigure(string name, StaticFileOptions options)
        {
            options = options ?? new StaticFileOptions();

            if (options.ContentTypeProvider.IsNull())
                options.ContentTypeProvider = new FileExtensionContentTypeProvider();

            if (options.FileProvider.IsNull())
            {
                if (Context.Environment.WebRootFileProvider.IsNull())
                    throw new InvalidOperationException("Missing FileProvider.");

                options.FileProvider = Context.Environment.WebRootFileProvider;
            }

            // Add our provider
            var fileProviders = new List<IFileProvider>();
            fileProviders.Add(options.FileProvider);

            AddStaticFileProviders(fileProviders);
            options.FileProvider = new CompositeFileProvider(fileProviders);
        }

        /// <summary>
        /// 添加静态文件提供程序集合。
        /// </summary>
        /// <param name="fileProviders">给定的当前 <see cref="IList{IFileProvider}"/>。</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "fileProviders")]
        protected virtual void AddStaticFileProviders(List<IFileProvider> fileProviders)
        {
            fileProviders.NotNull(nameof(fileProviders));

            //var staticFileProviders = Context.Themepack.Infos.Select(t => t.Value.GetStaticFileProvider());
            fileProviders.Add(Context.CurrentThemepackInfo.GetStaticFileProvider());
        }

    }
}
