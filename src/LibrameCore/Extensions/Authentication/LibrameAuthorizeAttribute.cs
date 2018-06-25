#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrameCore.Extensions.Authentication
{
    /// <summary>
    /// Librame 鉴权特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class LibrameAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter, IOrderedFilter
    {
        /// <summary>
        /// 构造一个 <see cref="LibrameAuthorizeAttribute"/> 实例。
        /// </summary>
        public LibrameAuthorizeAttribute()
        {
            AuthenticationSchemes = AuthenticationExtensionOptions.DEFAULT_SCHEME;
        }


        /// <summary>
        /// 排序。
        /// </summary>
        public int Order { get; set; }


        /// <summary>
        /// 开始鉴权。
        /// </summary>
        /// <param name="context">给定的授权过滤器上下文。</param>
        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            var policy = context.HttpContext.RequestServices.GetRequiredService<IAuthenticationPolicy>();
            
            // 开始认证
            var auth = policy.Authenticate(context.HttpContext);
            
            
            if (auth.Identity == null || !auth.Identity.IsAuthenticated)
            {
                if (context.HttpContext.Request.IsAjaxRequest())
                {
                    context.Result = new JsonResult(new { message = "未登录" });
                }
                else
                {
                    // 未登录
                    var redirectUrl = LocalPathToAbsoluteUrl(local => local.LoginPath, context, policy);
                    context.Result = new RedirectResult(redirectUrl);
                }
                return;
            }
            
            if (!string.IsNullOrEmpty(Roles))
            {
                var userRoles = auth.Identity.Roles;
                var requiredRoles = Roles.Split(',');

                // 验证角色（忽略大小写）
                if (!userRoles.Any(ur => requiredRoles.Any(rr => rr.Equals(ur, StringComparison.OrdinalIgnoreCase))))
                {
                    // 权限不足
                    var redirectUrl = LocalPathToAbsoluteUrl(local => local.AccessDeniedPath, context, policy);
                    context.Result = new RedirectResult(redirectUrl);
                    return;
                }
            }
        }


        /// <summary>
        /// 将本地配置的路径转换为 URL（自动附加当前请求路径为返回路径）。
        /// </summary>
        /// <param name="localPathFactory">给定的要转换的路径（支持绝对、相对路径）。</param>
        /// <param name="context">给定的 <see cref="AuthorizationFilterContext"/>。</param>
        /// <param name="policy">给定的 <see cref="IAuthenticationPolicy"/>。</param>
        /// <returns>返回 URL。</returns>
        protected virtual string LocalPathToAbsoluteUrl(Func<AuthenticationLocalOptions, string> localPathFactory,
            AuthorizationFilterContext context, IAuthenticationPolicy policy)
        {
            var requestUrl = context.HttpContext.Request.AsAbsoluteUrl();

            var localPath = localPathFactory.Invoke(policy.Options.Local);

            return localPath.AsAbsoluteUrl(context.HttpContext.Request,
                policy.Options.ReturnUrlParameter,
                new KeyValuePair<string, string>(policy.Options.ReturnUrlParameter, requestUrl));
        }

    }
}
