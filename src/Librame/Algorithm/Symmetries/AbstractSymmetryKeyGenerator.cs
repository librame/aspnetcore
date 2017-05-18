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
using System;
using System.Linq;

namespace Librame.Algorithm.Symmetries
{
    using Codecs;
    using Utility;

    /// <summary>
    /// 抽象对称算法密钥生成器。
    /// </summary>
    public abstract class AbstractSymmetryKeyGenerator : AbstractAlgorithm, ISymmetryKeyGenerator
    {
        private readonly string _defaultKeyString;

        /// <summary>
        /// 构造一个散列算法实例。
        /// </summary>
        /// <param name="logger">给定的记录器接口。</param>
        /// <param name="options">给定的选择项。</param>
        /// <param name="plainText">给定的明文编解码器接口。</param>
        /// <param name="cipherText">给定的密文编解码器接口。</param>
        /// <param name="keyString">给定的密钥字符串。</param>
        public AbstractSymmetryKeyGenerator(ILogger logger, IOptions<LibrameOptions> options,
            IPlainTextCodec plainText, ICipherTextCodec cipherText, string keyString)
            : base(logger, options, plainText, cipherText)
        {
            _defaultKeyString = keyString.NotEmpty(nameof(keyString));
        }

        
        /// <summary>
        /// 转换基础字节数组为所需的密钥字节数组。
        /// </summary>
        /// <param name="baseBytes">给定的基础字节数组。</param>
        /// <param name="bytesLength">给定的密钥字节数组长度。</param>
        /// <returns>返回密钥字节数组。</returns>
        protected abstract byte[] ConvertKeyBytes(byte[] baseBytes, int bytesLength);

        /// <summary>
        /// 转换密钥字节数组为所需的向量字节数组。
        /// </summary>
        /// <param name="key">给定的密钥。</param>
        /// <param name="bytesLength">给定的向量字节数组长度。</param>
        /// <returns>返回向量字节数组。</returns>
        protected abstract byte[] ConvertIvBytes(byte[] key, int bytesLength);


        /// <summary>
        /// 解析基础字节数组。
        /// </summary>
        /// <param name="keyString">给定的密钥字符串。</param>
        /// <returns>返回基础字节数组。</returns>
        protected virtual byte[] ParseBaseBytes(string keyString)
        {
            return CipherText.GetBytes(keyString);
        }

        /// <summary>
        /// 检测当前的比特大小是否有效。
        /// </summary>
        /// <param name="bitSize">给定的比特大小。</param>
        /// <returns>返回布尔值。</returns>
        protected virtual bool IsEffectiveBitSize(int bitSize)
        {
            return (bitSize % 8 == 0);
        }

        /// <summary>
        /// 获取指定比特大小对应的字节数组长度。
        /// </summary>
        /// <param name="bitSize">给定的比特大小。</param>
        /// <returns>返回字节数组长度。</returns>
        protected virtual int GetBytesLength(int bitSize)
        {
            // 比特大小除以 8 即表示当前算法所需的向量字节数组长度
            return (bitSize / 8);
        }


        /// <summary>
        /// 生成密钥。
        /// </summary>
        /// <param name="bitSize">给定要生成的密钥比特大小。</param>
        /// <param name="keyString">给定的密钥字符串（可选）。</param>
        /// <returns>返回密钥字节数组。</returns>
        protected virtual byte[] GenerateKey(int bitSize, string keyString = null)
        {
            // 解析密钥字符串的基础字节数组
            var baseBytes = ParseBaseBytes(keyString.AsOrDefault(_defaultKeyString));

            // 如果指定的比特大小无效，则抛出异常
            if (!IsEffectiveBitSize(bitSize))
            {
                var ex = new ArgumentException("bitSize must be in multiples of 8.");
                Logger.LogError(ex.AsInnerMessage());

                throw ex;
            }
            
            // 获取对应的密钥字节数组长度
            var bytesLength = GetBytesLength(bitSize);

            // 如果基础字节数组长度与所需的密钥字节数组长度相同，则不作处理直接返回
            if (baseBytes.Length == bytesLength)
                return baseBytes;

            // 根据不同的长度作自定义转换处理
            return ConvertKeyBytes(baseBytes, bytesLength);
        }
        
        /// <summary>
        /// 生成向量。
        /// </summary>
        /// <param name="key">给定的密钥字节数组。</param>
        /// <param name="bitSize">给定要生成的向量比特大小。</param>
        /// <returns>返回向量字节数组。</returns>
        protected virtual byte[] GenerateIv(byte[] key, int bitSize)
        {
            key.NotNull(nameof(key));

            // 获取对应的向量字节数组长度
            var bytesLength = GetBytesLength(bitSize);

            // 如果密钥字节数组长度与所需的向量字节数组长度相同，则直接倒序返回
            if (key.Length == bytesLength)
                return key.Reverse().ToArray();

            // 根据不同的长度作自定义转换处理
            return ConvertIvBytes(key, bytesLength);
        }


        /// <summary>
        /// 生成 AES 密钥。
        /// </summary>
        /// <param name="keyString">给定的密钥字符串（可选）。</param>
        /// <returns>返回密钥字节数组。</returns>
        public virtual byte[] GenerateAesKey(string keyString = null)
        {
            // 默认 AES 密钥的比特大小为 256 位
            return GenerateKey(256, keyString);
        }

        /// <summary>
        /// 生成 AES 向量。
        /// </summary>
        /// <param name="key">给定的密钥字节数组。</param>
        /// <returns>返回向量字节数组。</returns>
        public virtual byte[] GenerateAesIv(byte[] key)
        {
            // 默认 AES 向量的比特大小为 128 位
            return GenerateIv(key, 128);
        }

    }
}
