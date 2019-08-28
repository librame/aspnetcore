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
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
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
    /// 用户界面应用后置配置选项基类。
    /// </summary>
    public class UiApplicationPostConfigureOptionsBase : IUiApplicationPostConfigureOptions,
        IPostConfigureOptions<StaticFileOptions>,
        IPostConfigureOptions<RazorPagesOptions>,
        IPostConfigureOptions<RazorViewEngineOptions>
    {
        /// <summary>
        /// 构造一个 <see cref="UiApplicationPostConfigureOptionsBase"/>。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="areaName">指定的区域名称。</param>
        protected UiApplicationPostConfigureOptionsBase(IApplicationContext context, string areaName)
        {
            Context = context.NotNull(nameof(context));
            AreaName = areaName;
        }


        /// <summary>
        /// 应用上下文。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IApplicationContext"/>。
        /// </value>
        public IApplicationContext Context { get; }

        /// <summary>
        /// 区域名称。
        /// </summary>
        public string AreaName { get; }


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
            options.Conventions.AuthorizeAreaFolder(AreaName, BuilderOptions.Safety.ManageAreaRelativePath);
            options.Conventions.AuthorizeAreaPage(AreaName, BuilderOptions.Safety.LogoutAreaRelativePath);

            var convention = GetPageApplicationModelConvention();
            if (convention.IsNotNull())
            {
                options.Conventions.AddAreaFolderApplicationModelConvention(AreaName,
                    "/",
                    model => convention.Apply(model));
            }

            var filter = GetPageFilter();
            if (filter.IsNotNull())
            {
                options.Conventions.AddAreaFolderApplicationModelConvention(AreaName,
                    BuilderOptions.Safety.ManageAreaRelativePath,
                    model => model.Filters.Add(filter));
            }
        }

        /// <summary>
        /// 获取页面应用模型约束。
        /// </summary>
        /// <returns>返回 <see cref="IPageApplicationModelConvention"/>。</returns>
        protected virtual IPageApplicationModelConvention GetPageApplicationModelConvention()
        {
            return new UiServiceWithUserPageApplicationModelConvention(Builder);
        }

        /// <summary>
        /// 获取异步页面过滤器。
        /// </summary>
        /// <returns>返回 <see cref="IAsyncPageFilter"/>。</returns>
        protected virtual IAsyncPageFilter GetPageFilter()
        {
            return new ExternalAuthenticationSchemesPageFilter(Builder, BuilderOptions);
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
            var staticFileProviders = Context.Themepacks.Select(t => t.Value.GetStaticFileProvider());

            fileProviders.AddRange(staticFileProviders);
        }

    }
}
