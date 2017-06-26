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
    /// 密码管理器接口。
    /// </summary>
    public interface IPasswordManager : IManager
    {
        /// <summary>
        /// 算法适配器。
        /// </summary>
        IAlgorithmAdapter Algorithm { get; }


        /// <summary>
        /// 编码密码。
        /// </summary>
        /// <param name="original">给定的原始密码。</param>
        /// <returns>返回经过编码的密码字符串。</returns>
        string Encode(string original);


        /// <summary>
        /// 验证密码。
        /// </summary>
        /// <param name="encode">给定的编码密码。</param>
        /// <param name="original">给定的原始密码。</param>
        /// <returns>返回是否有效的布尔值。</returns>
        bool Validate(string encode, string original);
    }
}
