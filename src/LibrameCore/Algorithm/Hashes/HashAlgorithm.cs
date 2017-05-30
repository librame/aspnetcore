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

namespace LibrameCore.Algorithm.Hashes
{
    using Asymmetries;
    using TextCodecs;
    using Utilities;

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
        /// <param name="asymmetry">给定的非对称算法接口。</param>
        public HashAlgorithm(ILogger<HashAlgorithm> logger, IOptions<LibrameOptions> options,
            IPlainTextCodec plainText, ICipherTextCodec cipherText, IRsaAsymmetryAlgorithm asymmetry)
            : base(logger, options, plainText, cipherText)
        {
            Asymmetry = asymmetry.NotNull(nameof(asymmetry));
        }


        /// <summary>
        /// 非对称算法。
        /// </summary>
        public IRsaAsymmetryAlgorithm Asymmetry { get; }


        /// <summary>
        /// 给算法签名。
        /// </summary>
        /// <param name="buffer">给定要签名的字节数组。</param>
        /// <param name="hashName">给定的散列算法名。</param>
        /// <returns>返回签名的字节数组。</returns>
        protected virtual byte[] Sign(byte[] buffer, HashAlgorithmName hashName)
        {
            return Asymmetry.Sign(buffer, hashName);
        }


        /// <summary>
        /// 转换为 MD5。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="isSigned">是否签名（默认不签名）。</param>
        /// <returns>返回散列字符串。</returns>
        public virtual string ToMd5(string str, bool isSigned = false)
        {
            var buffer = Plain.GetBytes(str);
            
            var hash = MD5.Create();
            buffer = hash.ComputeHash(buffer);

            if (isSigned)
                buffer = Sign(buffer, HashAlgorithmName.MD5);

            return Cipher.GetString(buffer);
        }


        /// <summary>
        /// 转换为 SHA1。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="isSigned">是否签名（默认不签名）。</param>
        /// <returns>返回散列字符串。</returns>
        public virtual string ToSha1(string str, bool isSigned = false)
        {
            var buffer = Plain.GetBytes(str);

            var hash = SHA1.Create();
            buffer = hash.ComputeHash(buffer);

            if (isSigned)
                buffer = Sign(buffer, HashAlgorithmName.SHA1);

            return Cipher.GetString(buffer);
        }


        /// <summary>
        /// 转换为 SHA256。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="isSigned">是否签名（默认不签名）。</param>
        /// <returns>返回散列字符串。</returns>
        public virtual string ToSha256(string str, bool isSigned = false)
        {
            var buffer = Plain.GetBytes(str);

            var hash = SHA256.Create();
            buffer = hash.ComputeHash(buffer);

            if (isSigned)
                buffer = Sign(buffer, HashAlgorithmName.SHA256);

            return Cipher.GetString(buffer);
        }


        /// <summary>
        /// 转换为 SHA384。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="isSigned">是否签名（默认不签名）。</param>
        /// <returns>返回散列字符串。</returns>
        public virtual string ToSha384(string str, bool isSigned = false)
        {
            var buffer = Plain.GetBytes(str);

            var hash = SHA384.Create();
            buffer = hash.ComputeHash(buffer);

            if (isSigned)
                buffer = Sign(buffer, HashAlgorithmName.SHA384);

            return Cipher.GetString(buffer);
        }


        /// <summary>
        /// 转换为 SHA512。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="isSigned">是否签名（默认不签名）。</param>
        /// <returns>返回散列字符串。</returns>
        public virtual string ToSha512(string str, bool isSigned = false)
        {
            var buffer = Plain.GetBytes(str);

            var hash = SHA512.Create();
            buffer = hash.ComputeHash(buffer);

            if (isSigned)
                buffer = Sign(buffer, HashAlgorithmName.SHA512);

            return Cipher.GetString(buffer);
        }

    }
}
