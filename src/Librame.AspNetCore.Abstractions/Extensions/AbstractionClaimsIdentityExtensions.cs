#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;

namespace System.Security.Claims
{
    /// <summary>
    /// 抽象声明集合身份静态扩展。
    /// </summary>
    public static class AbstractionClaimsIdentityExtensions
    {
        /// <summary>
        /// 确保已认证名称。
        /// </summary>
        /// <param name="identity">给定的 <see cref="IIdentity"/>。</param>
        /// <param name="matchName">给定断定名称的声明工厂方法（可选）。</param>
        /// <returns>返回字符串。</returns>
        public static string EnsureAuthenticatedName(this IIdentity identity, Predicate<Claim> matchName = null)
            => (identity is ClaimsIdentity claimsIdentity) ? claimsIdentity.EnsureAuthenticatedName(matchName) : "Unsupported Identity";

        /// <summary>
        /// 确保已认证名称。
        /// </summary>
        /// <param name="identity">给定的 <see cref="ClaimsIdentity"/>。</param>
        /// <param name="matchName">给定断定名称的声明工厂方法（可选）。</param>
        /// <returns>返回字符串。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static string EnsureAuthenticatedName(this ClaimsIdentity identity, Predicate<Claim> matchName = null)
        {
            identity.NotNull(nameof(identity));

            if (identity.IsAuthenticated == false)
                return "Unauthorized Identity";

            if (matchName.IsNull())
                matchName = claim => "name".Equals(claim.Type, StringComparison.OrdinalIgnoreCase);

            return identity.Name.NotEmptyOrDefault(() => identity.FindFirst(matchName)?.Value);
        }

    }
}
