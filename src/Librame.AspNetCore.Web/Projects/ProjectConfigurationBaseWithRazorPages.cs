#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Projects
{
    using AspNetCore.Web.Applications;
    using Extensions;

    /// <summary>
    /// 带 <see cref="RazorPagesOptions"/> 的项目配置基类。
    /// </summary>
    public class ProjectConfigurationBaseWithRazorPages : ProjectConfigurationBase,
        IPostConfigureOptions<RazorPagesOptions>
    {
        /// <summary>
        /// 构造一个 <see cref="ProjectConfigurationBase"/>。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="area">给定的所属区域。</param>
        protected ProjectConfigurationBaseWithRazorPages(IApplicationContext context, string area)
            : base(context, area)
        {
        }


        /// <summary>
        /// 后置配置。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="options">给定的 <see cref="RazorPagesOptions"/>。</param>
        public virtual void PostConfigure(string name, RazorPagesOptions options)
        {
            options = options ?? new RazorPagesOptions();

            ConfigurePageConventions(options.Conventions);
        }

        /// <summary>
        /// 配置页面约束。
        /// </summary>
        /// <param name="conventions">给定的 <see cref="PageConventionCollection"/>。</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        protected virtual void ConfigurePageConventions(PageConventionCollection conventions)
        {
            conventions.NotNull(nameof(conventions));

            var convention = GetPageApplicationModelConvention();
            if (convention.IsNotNull())
            {
                conventions.AddAreaFolderApplicationModelConvention(Area,
                    "/",
                    model => convention.Apply(model));
            }
        }

        /// <summary>
        /// 获取页面应用模型约定（使用泛型页面应用模型）。
        /// </summary>
        /// <returns>返回 <see cref="IPageApplicationModelConvention"/>。</returns>
        protected virtual IPageApplicationModelConvention GetPageApplicationModelConvention()
            => new GenericPageApplicationModelConvention(Builder);
    }
}
