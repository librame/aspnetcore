#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;

    /// <summary>
    /// 密码规则提示助手。
    /// </summary>
    public class PasswordRulePromptHelper
    {
        /// <summary>
        /// 获取 HTML 内容。
        /// </summary>
        /// <param name="options">给定的 <see cref="IdentityOptions"/>。</param>
        /// <param name="localizer">给定的 <see cref="IExpressionHtmlLocalizer{RegisterViewResource}"/>。</param>
        /// <returns>返回字符串。</returns>
        public static string GetHtmlContent(IdentityOptions options, IExpressionHtmlLocalizer<RegisterViewResource> localizer)
        {
            var prompts = new StringBuilder();

            prompts.Append(localizer[r => r.PasswordRequiredLength].Value);
            prompts.Append(localizer[r => r.PasswordRequiredUniqueChars].Value);

            if (options.Password.RequireNonAlphanumeric)
                prompts.Append(localizer[r => r.PasswordRequireNonAlphanumeric].Value);

            if (options.Password.RequireLowercase)
                prompts.Append(localizer[r => r.PasswordRequireLowercase].Value);

            if (options.Password.RequireLowercase)
                prompts.Append(localizer[r => r.PasswordRequireUppercase].Value);

            if (options.Password.RequireLowercase)
                prompts.Append(localizer[r => r.PasswordRequireDigit].Value);

            return prompts.ToString();
        }

    }
}
