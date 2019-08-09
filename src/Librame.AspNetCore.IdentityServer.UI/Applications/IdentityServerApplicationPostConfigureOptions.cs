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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.IdentityServer.UI
{
    using AspNetCore.UI;

    /// <summary>
    /// 身份服务器应用后置配置选项。
    /// </summary>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    public class IdentityServerApplicationPostConfigureOptions<TUser> : UserInterfaceApplicationPostConfigureOptionsWithCookieBase
        where TUser : class
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityServerApplicationPostConfigureOptions{TUser}"/>。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        public IdentityServerApplicationPostConfigureOptions(IApplicationContext context)
            : this(context, "IdentityServer")
        {
        }

        /// <summary>
        /// 构造一个 <see cref="IdentityServerApplicationPostConfigureOptions{TUser}"/>。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="areaName">指定的区域名称。</param>
        protected IdentityServerApplicationPostConfigureOptions(IApplicationContext context, string areaName)
            : base(context, areaName)
        {
        }

        
        /// <summary>
        /// 后置配置。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="options">给定的 <see cref="CookieAuthenticationOptions"/>。</param>
        public override void PostConfigure(string name, CookieAuthenticationOptions options)
        {
            options = options ?? new CookieAuthenticationOptions();

            if (IdentityConstants.ApplicationScheme.Equals(name, StringComparison.Ordinal))
            {
                options.LoginPath = $"/{AreaName}/Account/Login";
                options.LogoutPath = $"/{AreaName}/Account/Logout";
                options.AccessDeniedPath = $"/{AreaName}/Account/AccessDenied";
            }
        }


        /// <summary>
        /// 后置配置。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="options">给定的 <see cref="RazorPagesOptions"/>。</param>
        public override void PostConfigure(string name, RazorPagesOptions options)
        {
            options = options ?? new RazorPagesOptions();

            options.AllowAreas = true;
            //options.Conventions.AuthorizeAreaFolder(AreaName, "/Account/Manage");
            options.Conventions.AuthorizeAreaPage(AreaName, "/Account/Logout");

            var convention = GetPageApplicationModelConvention();
            options.Conventions.AddAreaFolderApplicationModelConvention(
                AreaName,
                "/",
                pam => convention.Apply(pam));
            //options.Conventions.AddAreaFolderApplicationModelConvention(
            //    AreaName,
            //    "/Account/Manage",
            //    pam => pam.Filters.Add(new IdentityServerApplicationExternalLoginsPageFilter<TUser>()));
        }

        /// <summary>
        /// 获取页面应用模型约束。
        /// </summary>
        /// <returns>返回 <see cref="IPageApplicationModelConvention"/>。</returns>
        protected override IPageApplicationModelConvention GetPageApplicationModelConvention()
        {
            return new PageApplicationModelConventionWithUser<TUser>();
        }

    }
}
