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
using System.Linq;

namespace Librame.AspNetCore.UI
{
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 界面配置。
    /// </summary>
    public class InterfaceConfiguration : AbstractInterfaceConfiguration,
        IPostConfigureOptions<CookieAuthenticationOptions>,
        IPostConfigureOptions<StaticFileOptions>,
        IPostConfigureOptions<RazorViewEngineOptions>
    {
        /// <summary>
        /// 构造一个 <see cref="InterfaceConfiguration"/>。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="area">给定的所属区域。</param>
        protected InterfaceConfiguration(IApplicationContext context, string area)
            : base(context)
        {
            Area = area.NotNullOrEmpty(nameof(area));
        }


        /// <summary>
        /// 所属区域。
        /// </summary>
        protected string Area { get; }

        /// <summary>
        /// 界面信息。
        /// </summary>

        protected IInterfaceInfo Info
            => Context.GetInterfaceInfo(Area);


        /// <summary>
        /// 构建器。
        /// </summary>
        protected IUiBuilder Builder
            => Context.ServiceFactory.GetRequiredService<IUiBuilder>();

        /// <summary>
        /// 构建器选项。
        /// </summary>
        protected UiBuilderOptions BuilderOptions
            => Context.ServiceFactory.GetRequiredService<IOptions<UiBuilderOptions>>().Value;


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
                options.LoginPath = Info.Sitemap.Login.NewArea(Area).Href;
                options.LogoutPath = Info.Sitemap.Logout.NewArea(Area).Href;
                options.AccessDeniedPath = Info.Sitemap.AccessDenied.NewArea(Area).Href;
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

            var expander = options.ViewLocationExpanders?.First(p => p is LanguageViewLocationExpander);
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
        protected virtual void AddStaticFileProviders(List<IFileProvider> fileProviders)
        {
            var staticFileProviders = Context.ThemepackInfos.Select(t => t.Value.GetStaticFileProvider());
            fileProviders.AddRange(staticFileProviders);
        }

    }
}
