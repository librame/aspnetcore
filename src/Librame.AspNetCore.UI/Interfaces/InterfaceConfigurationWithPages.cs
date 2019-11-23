#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 带 <see cref="RazorPagesOptions"/> 的界面配置。
    /// </summary>
    public class InterfaceConfigurationWithPages : InterfaceConfiguration,
        IPostConfigureOptions<RazorPagesOptions>
    {
        /// <summary>
        /// 构造一个 <see cref="InterfaceConfiguration"/>。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="area">给定的所属区域。</param>
        protected InterfaceConfigurationWithPages(IApplicationContext context, string area)
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
            //options.AllowAreas = Area.IsNotEmpty();

            ConfigurePageConventions(options.Conventions);
        }

        /// <summary>
        /// 配置页面约束。
        /// </summary>
        /// <param name="conventions">给定的 <see cref="PageConventionCollection"/>。</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "conventions")]
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
        /// 获取页面应用模型约束。
        /// </summary>
        /// <returns>返回 <see cref="IPageApplicationModelConvention"/>。</returns>
        protected virtual IPageApplicationModelConvention GetPageApplicationModelConvention()
            => new GenericPageModelConventionWithUser(Builder);
    }
}
