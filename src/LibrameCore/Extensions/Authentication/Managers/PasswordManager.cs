#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Extensions.Algorithm;
using LibrameStandard.Utilities;
using Microsoft.Extensions.Options;

namespace LibrameCore.Extensions.Authentication.Managers
{
    /// <summary>
    /// 密码管理器。
    /// </summary>
    public class PasswordManager : AbstractAuthenticationExtensionService<PasswordManager>, IPasswordManager
    {
        /// <summary>
        /// 构造一个 <see cref="PasswordManager"/> 实例。
        /// </summary>
        /// <param name="hash">给定的 <see cref="IHashAlgorithm"/>。</param>
        /// <param name="options">给定的认证选项。</param>
        public PasswordManager(IHashAlgorithm hash,
            IOptionsMonitor<AuthenticationExtensionOptions> options)
            : base(options)
        {
            Hash = hash.NotNull(nameof(hash));
        }


        /// <summary>
        /// 哈希算法。
        /// </summary>
        public IHashAlgorithm Hash { get; }

        /// <summary>
        /// 对称算法。
        /// </summary>
        public ISymmetryAlgorithm Symmetry => Hash.Symmetry;


        /// <summary>
        /// 编码密码。
        /// </summary>
        /// <param name="original">给定的原始密码。</param>
        /// <returns>返回经过编码的密码字符串。</returns>
        public virtual string Encode(string original)
        {
            if (string.IsNullOrEmpty(original))
                return string.Empty;

            // SHA384散列算法（64位长度）
            var encode = Hash.GetSHA384(original);

            // AES动态加密（防撞库）
            encode = Symmetry.ToAES(encode);

            return encode;
        }


        /// <summary>
        /// 验证密码。
        /// </summary>
        /// <param name="encode">给定的编码密码。</param>
        /// <param name="original">给定的原始密码。</param>
        /// <returns>返回是否有效的布尔值。</returns>
        public virtual bool Validate(string encode, string original)
        {
            if (string.IsNullOrEmpty(encode) || string.IsNullOrEmpty(original))
                return false;

            // AES动态解密
            encode = Symmetry.FromAES(encode);

            // SHA384散列算法（64位长度）
            var compare = Hash.GetSHA384(original);

            return (encode == compare);
        }

    }
}
