#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Applications
{
    using AspNetCore.Identity.Builders;
    using AspNetCore.Web.Builders;
    using AspNetCore.Web.Services;
    using Extensions.Core.Services;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class IdentityApplicationPrincipal : AbstractApplicationPrincipal
    {
        public IdentityApplicationPrincipal(IWebBuilder webBuilder,
            IUserPortraitService userPortrait)
            : base(webBuilder, userPortrait)
        {
        }


        protected override dynamic GetSignInManager(HttpContext context)
        {
            // 因 IWebBuilder 从第二次配置开始会使用 IExtensionBuilderAdapter<IWebBuilder> 适配器模式，
            // 所以使用 Builder.GetRequiredBuilder<IIdentityServerBuilderDecorator>() 可能会不存在
            var decorator = context.RequestServices.GetRequiredService<IIdentityBuilderDecorator>();
            var managerType = typeof(SignInManager<>).MakeGenericType(decorator.Source.UserType);

            return context?.RequestServices?.GetRequiredService(managerType);
        }

    }
}
