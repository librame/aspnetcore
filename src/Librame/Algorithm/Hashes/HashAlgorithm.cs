#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Librame.Algorithm.Hashes
{
    using Asymmetries;
    using Symmetries;
    using Codecs;

    /// <summary>
    /// 散列算法。
    /// </summary>
    public class HashAlgorithm : AbstractAlgorithm, IHashAlgorithm
    {
        /// <summary>
        /// 构造一个散列算法实例。
        /// </summary>
        /// <param name="logger">给定的记录器接口。</param>
        /// <param name="options">给定的选择项。</param>
        /// <param name="plainText">给定的明文编解码器接口。</param>
        /// <param name="cipherText">给定的密文编解码器接口。</param>
        public HashAlgorithm(ILogger<HashAlgorithm> logger, IOptions<LibrameOptions> options,
            IPlainTextCodec plainText, ICipherTextCodec cipherText)
            : base(logger, options, plainText, cipherText)
        {
        }

        /// <summary>
        /// 给算法签名。
        /// </summary>
        /// <param name="rsa">给定的 RSA 非对称算法接口。</param>
        /// <param name="buffer">给定要签名的字节数组。</param>
        /// <param name="hashName">给定的散列算法名。</param>
        /// <returns>返回签名的字节数组。</returns>
        protected virtual byte[] Sign(IRsaAsymmetryAlgorithm rsa, ISymmetryAlgorithm sa, byte[] buffer, HashAlgorithmName hashName)
        {
            return rsa.Sign(buffer, hashName, sa);
        }


        /// <summary>
        /// 转换为 MD5。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="rsa">给定的 RSA 签名算法接口（可选；默认为空表示不签名）。</param>
        /// <returns>返回散列字符串。</returns>
        public virtual string ToMd5(string str, IRsaAsymmetryAlgorithm rsa = null, ISymmetryAlgorithm sa = null)
        {
            var buffer = PlainText.GetBytes(str);
            
            var hash = MD5.Create();
            buffer = hash.ComputeHash(buffer);

            if (rsa != null)
                buffer = Sign(rsa, buffer, HashAlgorithmName.MD5);

            return CipherText.GetString(buffer);
        }


        /// <summary>
        /// 转换为 SHA1。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="rsa">给定的 RSA 签名算法接口（可选；默认为空表示不签名）。</param>
        /// <returns>返回散列字符串。</returns>
        public virtual string ToSha1(string str, IRsaAsymmetryAlgorithm rsa = null, ISymmetryAlgorithm sa = null)
        {
            var buffer = PlainText.GetBytes(str);

            var hash = SHA1.Create();
            buffer = hash.ComputeHash(buffer);

            if (rsa != null)
                buffer = Sign(rsa, buffer, HashAlgorithmName.SHA1);

            return CipherText.GetString(buffer);
        }


        /// <summary>
        /// 转换为 SHA256。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="rsa">给定的 RSA 签名算法接口（可选；默认为空表示不签名）。</param>
        /// <returns>返回散列字符串。</returns>
        public virtual string ToSha256(string str, IRsaAsymmetryAlgorithm rsa = null, ISymmetryAlgorithm sa = null)
        {
            var buffer = PlainText.GetBytes(str);

            var hash = SHA256.Create();
            buffer = hash.ComputeHash(buffer);

            if (rsa != null)
                buffer = Sign(rsa, buffer, HashAlgorithmName.SHA256);

            return CipherText.GetString(buffer);
        }


        /// <summary>
        /// 转换为 SHA384。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="rsa">给定的 RSA 签名算法接口（可选；默认为空表示不签名）。</param>
        /// <returns>返回散列字符串。</returns>
        public virtual string ToSha384(string str, IRsaAsymmetryAlgorithm rsa = null, ISymmetryAlgorithm sa = null)
        {
            var buffer = PlainText.GetBytes(str);

            var hash = SHA384.Create();
            buffer = hash.ComputeHash(buffer);

            if (rsa != null)
                buffer = Sign(rsa, buffer, HashAlgorithmName.SHA384);

            return CipherText.GetString(buffer);
        }


        /// <summary>
        /// 转换为 SHA512。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="rsa">给定的 RSA 签名算法接口（可选；默认为空表示不签名）。</param>
        /// <returns>返回散列字符串。</returns>
        public virtual string ToSha512(string str, IRsaAsymmetryAlgorithm rsa = null, ISymmetryAlgorithm sa = null)
        {
            var buffer = PlainText.GetBytes(str);

            var hash = SHA512.Create();
            buffer = hash.ComputeHash(buffer);

            if (rsa != null)
                buffer = Sign(rsa, buffer, HashAlgorithmName.SHA512);

            return CipherText.GetString(buffer);
        }

    }
}
