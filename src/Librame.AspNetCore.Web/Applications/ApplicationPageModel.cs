#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Web.Applications
{
    using AspNetCore.Web.Descriptors;
    using AspNetCore.Web.Projects;
    using Extensions;
    using Extensions.Core.Services;

    /// <summary>
    /// 应用页面模型。
    /// </summary>
    public class ApplicationPageModel : PageModel
    {
        /// <summary>
        /// 构造一个 <see cref="ApplicationPageModel"/>。
        /// </summary>
        /// <param name="injection">给定的 <see cref="IInjectionService"/>。</param>
        protected ApplicationPageModel(IInjectionService injection)
            : base()
        {
            injection.NotNull(nameof(injection)).Inject(this);

            Application.SetCurrentProject(HttpContext);
        }


        /// <summary>
        /// 应用上下文。
        /// </summary>
        [InjectionService]
        public IApplicationContext Application { get; set; }


        /// <summary>
        /// 状态消息。
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }


        /// <summary>
        /// 添加模型错误集合。
        /// </summary>
        /// <param name="result">给定的 <see cref="IdentityResult"/>。</param>
        protected virtual void AddModelErrors(IdentityResult result)
        {
            foreach (var error in result?.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }


        /// <summary>
        /// 获取区域导航路径。
        /// </summary>
        /// <param name="navigationFactory">指定定位导航的工厂方法。</param>
        /// <returns>返回字符串。</returns>
        public virtual string GetAreaNavigationPath(Func<IProjectNavigation, NavigationDescriptor> navigationFactory)
            => navigationFactory?.Invoke(Application.CurrentProject.Navigation)?.GenerateLink(Url);


        /// <summary>
        /// 重定向到区域导航首页。
        /// </summary>
        /// <returns>返回 <see cref="RedirectResult"/>。</returns>
        public virtual RedirectResult RedirectToAreaNavigationIndex()
            => RedirectToAreaNavigation(nav => nav.Index);

        /// <summary>
        /// 重定向到区域导航。
        /// </summary>
        /// <param name="navigationFactory">指定定位导航的工厂方法。</param>
        /// <returns>返回 <see cref="RedirectResult"/>。</returns>
        public virtual RedirectResult RedirectToAreaNavigation(Func<IProjectNavigation, NavigationDescriptor> navigationFactory)
            => Redirect(GetAreaNavigationPath(navigationFactory));

        /// <summary>
        /// 重定向到本地 URL 或默认路径。
        /// </summary>
        /// <param name="returnUrl">给定的返回 URL。</param>
        /// <param name="defaultPath">给定的默认路径（可选）。</param>
        /// <returns>返回 <see cref="RedirectResult"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        public virtual RedirectResult RedirectToLocalUrlOrDefaultPath(string returnUrl, string defaultPath = null)
        {
            if (returnUrl.IsEmpty() && defaultPath.IsNotEmpty())
                return Redirect(defaultPath);

            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            // 如果默认路径为空，则使用站点地图首页路径
            return Redirect(defaultPath.NotEmptyOrDefault(() => GetAreaNavigationPath(nav => nav.Index)));
        }


        /// <summary>
        /// 验证登入用户动作结果。
        /// </summary>
        /// <param name="userManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="actionResultFactory">给定的动作结果工厂方法。</param>
        /// <returns>返回 <see cref="Task{IActionResult}"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual async Task<IActionResult> VerifyLoginUserActionResult<TUser>(UserManager<TUser> userManager,
            Func<TUser, IActionResult> actionResultFactory)
            where TUser : class
        {
            (TUser user, IActionResult result) = await GetLoginUserAsync(userManager).ConfigureAndResultAsync();

            if (result.IsNotNull())
                return result;

            return actionResultFactory?.Invoke(user);
        }

        /// <summary>
        /// 验证登入用户动作结果。
        /// </summary>
        /// <param name="userManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="actionResultFactory">给定的动作结果工厂方法。</param>
        /// <returns>返回 <see cref="Task{IActionResult}"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual async Task<IActionResult> VerifyLoginUserActionResult<TUser>(UserManager<TUser> userManager,
            Func<TUser, Task<IActionResult>> actionResultFactory)
            where TUser : class
        {
            (TUser user, IActionResult result) = await GetLoginUserAsync(userManager).ConfigureAndResultAsync();

            if (result.IsNotNull())
                return result;

            return await (actionResultFactory?.Invoke(user)).ConfigureAndResultAsync();
        }

        private async Task<(TUser user, IActionResult result)> GetLoginUserAsync<TUser>(UserManager<TUser> userManager)
            where TUser : class
        {
            userManager.NotNull(nameof(userManager));

            var user = await userManager.GetUserAsync(User).ConfigureAndResultAsync();
            if (user.IsNull())
            {
                var options = Application.ServiceFactory.GetRequiredService<IOptions<CookieAuthenticationOptions>>().Value;
                var returnUrlPair = $"{options.ReturnUrlParameter}={Request.Path}";

                var redirectResult = RedirectToAreaNavigation(nav =>
                {
                    var descr = nav.IdentityNavigation?.Login ?? nav.RootNavigation.Index;
                    return descr.WithRoute<PageRouteDescriptor>(r => r.WithId(returnUrlPair));
                });
                //return NotFound($"Unable to load user with ID '{UserManager.GetUserId(User)}'.");

                return (null, redirectResult);
            }

            return (user, null);
        }

    }
}
