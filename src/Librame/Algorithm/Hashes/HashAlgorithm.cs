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
    using Utility;

    /// <summary>
    /// 散列算法。
    /// </summary>
    public class HashAlgorithm : AbstractAlgorithm, IHashAlgorithm
    {
        /// <summary>
        /// 构造一个散列算法实例。
        /// </summary>
        /// <param name="byteConverter">给定的字节转换器接口。</param>
        /// <param name="logger">给定的记录器工厂接口。</param>
        /// <param name="options">给定的选择项。</param>
        public HashAlgorithm(ICiphertextCodec byteConverter,
            ILogger<HashAlgorithm> logger, IOptions<LibrameOptions> options)
            : base(logger, options)
        {
            ByteConverter = byteConverter.NotNull(nameof(byteConverter));
        }


        /// <summary>
        /// 字节转换器接口。
        /// </summary>
        public ICiphertextCodec ByteConverter { get; }


        /// <summary>
        /// 转换为 MD5。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回散列字符串。</returns>
        public virtual string ToMd5(string str)
        {
            var buffer = EncodeBytes(str);
            
            var hash = MD5.Create();
            buffer = hash.ComputeHash(buffer);

            return ByteConverter.AsString(buffer);
        }


        /// <summary>
        /// 转换为 SHA1。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回散列字符串。</returns>
        public virtual string ToSha1(string str)
        {
            var buffer = EncodeBytes(str);

            var hash = SHA1.Create();
            buffer = hash.ComputeHash(buffer);

            return ByteConverter.AsString(buffer);
        }


        /// <summary>
        /// 转换为 SHA256。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回散列字符串。</returns>
        public virtual string ToSha256(string str)
        {
            var buffer = EncodeBytes(str);

            var hash = SHA256.Create();
            buffer = hash.ComputeHash(buffer);

            return ByteConverter.AsString(buffer);
        }


        /// <summary>
        /// 转换为 SHA384。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回散列字符串。</returns>
        public virtual string ToSha384(string str)
        {
            var buffer = EncodeBytes(str);

            var hash = SHA384.Create();
            buffer = hash.ComputeHash(buffer);

            return ByteConverter.AsString(buffer);
        }


        /// <summary>
        /// 转换为 SHA512。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回散列字符串。</returns>
        public virtual string ToSha512(string str)
        {
            var buffer = EncodeBytes(str);

            var hash = SHA512.Create();
            buffer = hash.ComputeHash(buffer);

            return ByteConverter.AsString(buffer);
        }

    }
}
