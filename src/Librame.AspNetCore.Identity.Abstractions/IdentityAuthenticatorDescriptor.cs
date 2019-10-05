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

namespace Librame.AspNetCore.Identity
{
    using Extensions;

    /// <summary>
    /// 身份认证器描述符。
    /// </summary>
    public class IdentityAuthenticatorDescriptor : IEquatable<IdentityAuthenticatorDescriptor>
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityAuthenticatorDescriptor"/>。
        /// </summary>
        /// <param name="schemeName">给定用于显示的方案名称。</param>
        /// <param name="accountName">给定的帐户名称。</param>
        /// <param name="secretKey">给定的秘钥。</param>
        /// <param name="issuer">给定的发行人（可选；默认为 <paramref name="schemeName"/>）。</param>
        /// <param name="passwordDigits">给定的密码位数（可选；默认为 6 位）。</param>
        public IdentityAuthenticatorDescriptor(string schemeName, string accountName,
            string secretKey, string issuer = null, int passwordDigits = 6)
        {
            SchemeName = schemeName.NotEmpty(nameof(schemeName));
            AccountName = accountName.NotEmpty(nameof(accountName));
            SecretKey = secretKey.NotEmpty(nameof(accountName));

            Issuer = issuer.NotEmptyOrDefault(SchemeName);
            PasswordDigits = passwordDigits;
        }


        /// <summary>
        /// 方案名称。
        /// </summary>
        public string SchemeName { get; }

        /// <summary>
        /// 帐户名称。
        /// </summary>
        public string AccountName { get; }

        /// <summary>
        /// 秘钥。
        /// </summary>
        public string SecretKey { get; }

        /// <summary>
        /// 发行人。
        /// </summary>
        public string Issuer { get; }
        
        /// <summary>
        /// 密码位数。
        /// </summary>
        public int PasswordDigits { get; }


        /// <summary>
        /// 建立 OTP 验证 URI 字符串。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public string BuildOtpAuthUriString()
            => $"otpauth://totp/{SchemeName}:{AccountName}?secret={SecretKey}&issuer={Issuer}&digits={PasswordDigits}";


        /// <summary>
        /// 是否相等。
        /// </summary>
        /// <param name="other">给定用于比较的 <see cref="IdentityAuthenticatorDescriptor"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(IdentityAuthenticatorDescriptor other)
            => ToString() == other?.ToString();

        /// <summary>
        /// 是否相等。
        /// </summary>
        /// <param name="obj">给定的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is IdentityAuthenticatorDescriptor other) ? Equals(other) : false;


        /// <summary>
        /// 获取哈希码。
        /// </summary>
        /// <returns>返回整数。</returns>
        public override int GetHashCode()
            => ToString().GetHashCode();


        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
            => BuildOtpAuthUriString();
    }
}
