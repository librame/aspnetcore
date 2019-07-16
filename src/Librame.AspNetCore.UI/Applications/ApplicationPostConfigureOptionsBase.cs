#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 应用程序后置配置选项基类。
    /// </summary>
    public class ApplicationPostConfigureOptionsBase : IApplicationPostConfigureOptionsBase,
        IPostConfigureOptions<StaticFileOptions>,
        IPostConfigureOptions<RazorPagesOptions>,
        IPostConfigureOptions<RazorViewEngineOptions>
    {
        /// <summary>
        /// 构造一个 <see cref="ApplicationPostConfigureOptionsBase"/> 实例。
        /// </summary>
        /// <param name="themepack">给定的 <see cref="IThemepackInfo"/>。</param>
        /// <param name="environment">给定的 <see cref="IHostingEnvironment"/>。</param>
        /// <param name="areaName">指定的区域名称。</param>
        protected ApplicationPostConfigureOptionsBase(IThemepackInfo themepack,
            IHostingEnvironment environment, string areaName)
        {
            Themepack = themepack.NotNull(nameof(themepack));
            Environment = environment.NotNull(nameof(environment));
            AreaName = areaName;
        }


        /// <summary>
        /// 主题包信息。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IThemepackInfo"/>。
        /// </value>
        public IThemepackInfo Themepack { get; }

        /// <summary>
        /// 主机环境。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IHostingEnvironment"/>。
        /// </value>
        public IHostingEnvironment Environment { get; }

        /// <summary>
        /// 区域名称。
        /// </summary>
        public string AreaName { get; }


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
        /// <param name="name">给定的名称。</param>
        /// <param name="options">给定的 <see cref="RazorPagesOptions"/>。</param>
        public virtual void PostConfigure(string name, RazorPagesOptions options)
        {
            options = options ?? new RazorPagesOptions();

            options.AllowAreas = AreaName.IsNotNullOrEmpty();
            
            var convention = GetPageApplicationModelConvention();
            if (convention.IsNull()) return;

            options.Conventions.AddAreaFolderApplicationModelConvention(
                AreaName,
                "/",
                pam => convention.Apply(pam));
        }

        /// <summary>
        /// 获取页面应用模型约束。
        /// </summary>
        /// <returns>返回 <see cref="IPageApplicationModelConvention"/>。</returns>
        protected virtual IPageApplicationModelConvention GetPageApplicationModelConvention()
        {
            return null;
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
                if (Environment.WebRootFileProvider.IsNull())
                    throw new InvalidOperationException("Missing FileProvider.");

                options.FileProvider = Environment.WebRootFileProvider;
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
        protected virtual void AddStaticFileProviders(IList<IFileProvider> fileProviders)
        {
            var staticFileProvider = Themepack.GetStaticFileProvider();
            fileProviders.Add(staticFileProvider);
        }

    }
}
