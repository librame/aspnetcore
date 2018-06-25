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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LibrameCore.Extensions.Authentication
{
    /// <summary>
    /// Librame 客户端注销特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class LibrameClientLogoutAttribute : LibrameAuthorizeAttribute
    {
        /// <summary>
        /// 构造一个 <see cref="LibrameClientLogoutAttribute"/> 实例。
        /// </summary>
        public LibrameClientLogoutAttribute()
            : base()
        {
        }


        /// <summary>
        /// 注销认证。
        /// </summary>
        /// <param name="context">给定的授权过滤器上下文。</param>
        public override void OnAuthorization(AuthorizationFilterContext context)
        {
            var policy = context.HttpContext.RequestServices.GetRequiredService<IAuthenticationPolicy>();

            // 开始认证
            var auth = policy.Authenticate(context.HttpContext);
            var requestUrl = context.HttpContext.Request.AsAbsoluteUrl();

            if (auth.Identity != null && auth.Identity.IsAuthenticated)
            {
                // 注销
                var redirectUrl = LocalPathToAbsoluteUrl(local => local.LogoutPath, context, policy);
                context.Result = new RedirectResult(redirectUrl);
                return;
            }
            else
            {
                if (context.HttpContext.Request.IsAjaxRequest())
                {
                    context.Result = new JsonResult(new { message = "未登录" });
                }
                else
                {
                    // 登录
                    var redirectUrl = LocalPathToAbsoluteUrl(local => local.LoginPath, context, policy);
                    context.Result = new RedirectResult(redirectUrl);
                }
                return;
            }
        }

    }
}
