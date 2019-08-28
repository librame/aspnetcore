#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Librame.AspNetCore.IdentityServer.UI
{
    using AspNetCore.UI;

    /// <summary>
    /// 身份服务器用户界面构建器静态扩展。
    /// </summary>
    public static class IdentityServerUiBuilderExtensions
    {
        /// <summary>
        /// 添加身份服务器用户界面扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IIdentityServerBuilderWrapper"/>。</param>
        /// <param name="setupAction">给定的选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        public static IUiBuilder AddIdentityServerUI(this IIdentityServerBuilderWrapper builder,
            Action<UiBuilderOptions> setupAction = null)
        {
            return builder.AddUI<IdentityServerApplicationPostConfigureOptions>(setupAction)
                .AddUser(builder.UserType);
        }

    }
}
