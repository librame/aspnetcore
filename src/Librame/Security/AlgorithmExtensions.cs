// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame;
using Librame.Security;
using Librame.Security.Hash;
using Librame.Security.Keying;
using Librame.Security.Symmetry;
using Microsoft.AspNet.Builder;
using System;
using System.Text;

namespace System
{
    /// <summary>
    /// 算法静态扩展。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class AlgorithmExtensions
    {
        #region Aes

        /// <summary>
        /// AES 加密。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回加密字符串。</returns>
        public static string AsAes(this string str)
        {
            KeyIvInfo keyIvInfo;

            return str.AsAes(out keyIvInfo);
        }
        /// <summary>
        /// AES 加密。
        /// </summary>
        /// <remarks>
        /// 支持输出密钥、向量等算法信息。
        /// </remarks>
        /// <param name="str">给定的字符串。</param>
        /// <param name="keyIvInfo">输出密钥向量信息。</param>
        /// <returns>返回加密字符串。</returns>
        public static string AsAes(this string str, out KeyIvInfo keyIvInfo)
        {
            if (String.IsNullOrEmpty(str))
            {
                keyIvInfo = null;
                return str;
            }

            var binder = LibrameHelper.GetOrCreateBinder();
            // 默认绑定项
            var aes = binder.Resolve<ISymmetryAlgorithm>(ApplicationBinder.AlgorithmDomain);
            keyIvInfo = aes.KeyIvInfo;

            return aes.Encode(str);
        }

        /// <summary>
        /// AES 解密。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="keyIvInfo">给定的密钥向量信息（可选）。</param>
        /// <returns>返回解密字符串。</returns>
        public static string FromAes(this string str, KeyIvInfo keyIvInfo = null)
        {
            ISymmetryAlgorithm aes = null;

            var binder = LibrameHelper.GetOrCreateBinder();

            if (ReferenceEquals(keyIvInfo, null))
            {
                // 默认绑定项
                aes = binder.Resolve<ISymmetryAlgorithm>(ApplicationBinder.AlgorithmDomain);
            }
            else
            {
                // 默认服务映射
                var encoding = binder.GetService<Encoding>();
                aes = new AesSymmetryAlgorithm(keyIvInfo.Key, keyIvInfo.Iv, encoding);
            }

            return aes.Decode(str);
        }
        /// <summary>
        /// AES 解密。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="unique">给定的唯一标识。</param>
        /// <returns>返回解密字符串。</returns>
        public static string FromAes(this string str, UniqueIdentity unique)
        {
            var binder = LibrameHelper.GetOrCreateBinder();

            if (ReferenceEquals(unique, null))
            {
                // 默认服务映射
                unique = binder.GetService<UniqueIdentity>();
            }

            // 默认服务映射
            var encoding = binder.GetService<Encoding>();
            var aes = new AesSymmetryAlgorithm(unique, encoding);

            return aes.Decode(str);
        }

        #endregion


        #region HMAC Hash

        /// <summary>
        /// 计算字符串的 HMAC SHA256 哈希值。 
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回哈希字符串。</returns>
        public static string AsHmacSha256(this string str)
        {
            KeyInfo keyInfo;

            return str.AsHmacSha256(out keyInfo);
        }
        /// <summary>
        /// 计算字符串的 HMAC SHA256 哈希值。 
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="keyInfo">输出密钥信息。</param>
        /// <returns>返回哈希字符串。</returns>
        public static string AsHmacSha256(this string str, out KeyInfo keyInfo)
        {
            if (String.IsNullOrEmpty(str))
            {
                keyInfo = null;
                return str;
            }

            var binder = LibrameHelper.GetOrCreateBinder();
            // 默认绑定项
            var hmacSha256 = binder.Resolve<IKeyedAlgorithm>(ApplicationBinder.AlgorithmDomain);
            keyInfo = hmacSha256.KeyInfo;

            return hmacSha256.Encode(str);
        }

        /// <summary>
        /// 是否为有效的 HMAC SHA256 哈希值。
        /// </summary>
        /// <param name="hmacString">给定的 HMAC SHA256 哈希字符串。</param>
        /// <param name="str">给定的原始字符串。</param>
        /// <param name="keyInfo">给定的密钥信息。</param>
        /// <returns>返回比较结果的布尔值。</returns>
        public static bool IsHmacSha256(this string hmacString, string str, KeyInfo keyInfo = null)
        {
            if (String.IsNullOrEmpty(hmacString) || String.IsNullOrEmpty(str))
            {
                return false;
            }

            IKeyedAlgorithm hmacSha256 = null;

            var binder = LibrameHelper.GetOrCreateBinder();

            if (ReferenceEquals(null, keyInfo))
            {
                // 默认绑定项
                hmacSha256 = binder.Resolve<IKeyedAlgorithm>(ApplicationBinder.AlgorithmDomain);
            }
            else
            {
                var encoding = binder.GetService<Encoding>();
                hmacSha256 = new HMACSHA256KeyedAlgorithm(keyInfo.Key, encoding);
            }

            return Equals(hmacString, hmacSha256.Encode(str));
        }

        #endregion


        #region Hash

        /// <summary>
        /// 计算字符串的 MD5 哈希值。 
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回哈希字符串。</returns>
        public static string AsMd5(this string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return str;
            }

            var binder = LibrameHelper.GetOrCreateBinder();
            var encoding = binder.GetService<Encoding>();

            var md5 = new MD5HashAlgorithm(encoding);
            return md5.Encode(str);
        }

        /// <summary>
        /// 计算字符串的 SHA1 哈希值。 
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回哈希字符串。</returns>
        public static string AsSha1(this string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return str;
            }

            var binder = LibrameHelper.GetOrCreateBinder();
            var encoding = binder.GetService<Encoding>();

            var sha1 = new SHA1HashAlgorithm(encoding);
            return sha1.Encode(str);
        }

        /// <summary>
        /// 计算字符串的 SHA256 哈希值。 
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回哈希字符串。</returns>
        public static string AsSha256(this string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return str;
            }

            var binder = LibrameHelper.GetOrCreateBinder();
            // 默认绑定项
            var sha256 = binder.Resolve<IAlgorithm>(ApplicationBinder.AlgorithmDomain);

            return sha256.Encode(str);
        }

        /// <summary>
        /// 计算字符串的 SHA384 哈希值。 
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回哈希字符串。</returns>
        public static string AsSha384(this string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return str;
            }

            var binder = LibrameHelper.GetOrCreateBinder();
            var encoding = binder.GetService<Encoding>();

            var sha384 = new SHA384HashAlgorithm(encoding);
            return sha384.Encode(str);
        }

        /// <summary>
        /// 计算字符串的 SHA512 哈希值。 
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回哈希字符串。</returns>
        public static string AsSha512(this string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return str;
            }

            var binder = LibrameHelper.GetOrCreateBinder();
            var encoding = binder.GetService<Encoding>();

            var sha512 = new SHA512HashAlgorithm(encoding);
            return sha512.Encode(str);
        }

        #endregion

    }
}