﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Localization;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Librame.AspNetCore.Identity.Web
{
    using Resources;

    /// <summary>
    /// 密码规则提示助手。
    /// </summary>
    public static class PasswordRulePromptHelper
    {
        /// <summary>
        /// 获取 HTML 内容。
        /// </summary>
        /// <param name="options">给定的 <see cref="IdentityOptions"/>。</param>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer{RegisterViewResource}"/>。</param>
        /// <returns>返回字符串。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static string GetHtmlContent(IdentityOptions options, IHtmlLocalizer<RegisterViewResource> localizer)
        {
            var prompts = new StringBuilder();

            prompts.Append(localizer.GetString(r => r.PasswordRequiredLength).Value);
            prompts.Append("<br />");
            prompts.Append(localizer.GetString(r => r.PasswordRequiredUniqueChars).Value);

            if (options.Password.RequireNonAlphanumeric)
            {
                prompts.Append("<br />");
                prompts.Append(localizer.GetString(r => r.PasswordRequireNonAlphanumeric).Value);
            }

            if (options.Password.RequireLowercase)
            {
                prompts.Append("<br />");
                prompts.Append(localizer.GetString(r => r.PasswordRequireLowercase).Value);
            }

            if (options.Password.RequireLowercase)
            {
                prompts.Append("<br />");
                prompts.Append(localizer.GetString(r => r.PasswordRequireUppercase).Value);
            }

            if (options.Password.RequireLowercase)
            {
                prompts.Append("<br />");
                prompts.Append(localizer.GetString(r => r.PasswordRequireDigit).Value);
            }

            return prompts.ToString();
        }

    }
}
