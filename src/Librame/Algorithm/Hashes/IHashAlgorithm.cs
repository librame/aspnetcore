#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Algorithm.Hashes
{
    /// <summary>
    /// 散列算法接口。
    /// </summary>
    public interface IHashAlgorithm : IByteCodec
    {
        /// <summary>
        /// 字节转换器接口。
        /// </summary>
        IByteConverter Converter { get; }


        /// <summary>
        /// 转换为 MD5。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回散列字符串。</returns>
        string ToMd5(string str);


        /// <summary>
        /// 转换为 SHA1。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回散列字符串。</returns>
        string ToSha1(string str);


        /// <summary>
        /// 转换为 SHA256。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回散列字符串。</returns>
        string ToSha256(string str);


        /// <summary>
        /// 转换为 SHA384。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回散列字符串。</returns>
        string ToSha384(string str);


        /// <summary>
        /// 转换为 SHA512。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回散列字符串。</returns>
        string ToSha512(string str);

    }
}
