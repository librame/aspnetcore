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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;

namespace Librame.AspNetCore.Identity.UI.Pages
{
    /// <summary>
    /// 内部 UI 身份选项。
    /// </summary>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    internal class InternalUIIdentityOptions<TUser> :
        IPostConfigureOptions<RazorPagesOptions>,
        IPostConfigureOptions<StaticFileOptions>,
        IPostConfigureOptions<CookieAuthenticationOptions> where TUser : class
    {
        private const string DefaultAreaName = "Identity";


        /// <summary>
        /// 构造一个 <see cref="InternalUIIdentityOptions{TUser}"/> 实例。
        /// </summary>
        /// <param name="environment">给定的 <see cref="IHostingEnvironment"/>。</param>
        public InternalUIIdentityOptions(IHostingEnvironment environment)
        {
            Environment = environment;
        }


        /// <summary>
        /// 主机环境。
        /// </summary>
        public IHostingEnvironment Environment { get; }


        /// <summary>
        /// 后置配置。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="options">给定的 <see cref="RazorPagesOptions"/>。</param>
        public void PostConfigure(string name, RazorPagesOptions options)
        {
            name = name ?? throw new ArgumentNullException(nameof(name));
            options = options ?? throw new ArgumentNullException(nameof(options));

            options.AllowAreas = true;
            options.Conventions.AuthorizeAreaFolder(DefaultAreaName, "/Account/Manage");
            options.Conventions.AuthorizeAreaPage(DefaultAreaName, "/Account/Logout");

            var convention = new InternalUIIdentityPageModelConvention<TUser>();
            options.Conventions.AddAreaFolderApplicationModelConvention(
                DefaultAreaName,
                "/",
                pam => convention.Apply(pam));
            options.Conventions.AddAreaFolderApplicationModelConvention(
                DefaultAreaName,
                "/Account/Manage",
                pam => pam.Filters.Add(new ExternalLoginsPageFilter<TUser>()));
        }


        /// <summary>
        /// 后置配置。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="options">给定的 <see cref="StaticFileOptions"/>。</param>
        public void PostConfigure(string name, StaticFileOptions options)
        {
            name = name ?? throw new ArgumentNullException(nameof(name));
            options = options ?? throw new ArgumentNullException(nameof(options));

            // Basic initialization in case the options weren't initialized by any other component
            options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();
            if (options.FileProvider == null && Environment.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider.");
            }

            options.FileProvider = options.FileProvider ?? Environment.WebRootFileProvider;

            // Add our provider
            var filesProvider = new ManifestEmbeddedFileProvider(GetType().Assembly, "wwwroot");
            options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);
        }


        /// <summary>
        /// 后置配置。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="options">给定的 <see cref="CookieAuthenticationOptions"/>。</param>
        public void PostConfigure(string name, CookieAuthenticationOptions options)
        {
            name = name ?? throw new ArgumentNullException(nameof(name));
            options = options ?? throw new ArgumentNullException(nameof(options));

            if (string.Equals(IdentityConstants.ApplicationScheme, name, StringComparison.Ordinal))
            {
                options.LoginPath = $"/{DefaultAreaName}/Account/Login";
                options.LogoutPath = $"/{DefaultAreaName}/Account/Logout";
                options.AccessDeniedPath = $"/{DefaultAreaName}/Account/AccessDenied";
            }
        }

    }
}
