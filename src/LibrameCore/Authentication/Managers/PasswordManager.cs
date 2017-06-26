#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace LibrameStandard.Authentication.Managers
{
    using Algorithm;

    /// <summary>
    /// 密码管理器。
    /// </summary>
    public class PasswordManager : AbstractManager, IPasswordManager
    {
        /// <summary>
        /// 构造一个密码管理器实例。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        public PasswordManager(ILibrameBuilder builder)
            : base(builder)
        {
        }


        /// <summary>
        /// 算法适配器。
        /// </summary>
        public IAlgorithmAdapter Algorithm => Builder.GetAlgorithmAdapter();


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
            var encode = Algorithm.Hash.ToSha384(original);

            // AES动态加密（防撞库）
            encode = Algorithm.Symmetry.ToAes(encode);

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
            encode = Algorithm.Symmetry.FromAes(encode);

            // SHA384散列算法（64位长度）
            var compare = Algorithm.Hash.ToSha384(original);

            return (encode == compare);
        }

    }
}
