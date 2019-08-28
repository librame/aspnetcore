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

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;

    /// <summary>
    /// 身份用户界面构建器静态扩展。
    /// </summary>
    public static class IdentityUiBuilderExtensions
    {
        /// <summary>
        /// 添加身份用户界面扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IIdentityBuilderWrapper"/>。</param>
        /// <param name="setupAction">给定的选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        public static IUiBuilder AddIdentityUI(this IIdentityBuilderWrapper builder,
            Action<UiBuilderOptions> setupAction = null)
        {
            return builder.AddUI<IdentityApplicationPostConfigureOptions>(setupAction)
                .AddUser(builder.RawBuilder.UserType);
        }

    }
}
