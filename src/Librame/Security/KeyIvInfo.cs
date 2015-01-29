// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Librame.Security
{
    /// <summary>
    /// 密钥向量信息。
    /// </summary>
    /// <author>Librame Pang</author>
    public class KeyIvInfo : KeyInfo
    {
        /// <summary>
        /// 构造一个密钥向量信息实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// keyString 与 ivString 为空或 <see cref="String.Empty"/>。
        /// </exception>
        /// <param name="keyString">给定的密钥字符串。</param>
        /// <param name="ivString">给定的向量字符串。</param>
        public KeyIvInfo(string keyString, string ivString)
            : base(keyString)
        {
            if (String.IsNullOrEmpty(ivString))
            {
                throw new ArgumentNullException("ivString");
            }

            Iv = WebEncoders.Base64UrlDecode(ivString);
            IvString = ivString;
        }
        /// <summary>
        /// 构造一个密钥向量信息实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// key 与 iv 为空。
        /// </exception>
        /// <param name="key">给定的密钥字节数组。</param>
        /// <param name="iv">给定的向量字符数组。</param>
        public KeyIvInfo(byte[] key, byte[] iv)
            : base(key)
        {
            if (ReferenceEquals(iv, null))
            {
                throw new ArgumentNullException("iv");
            }

            IvString = WebEncoders.Base64UrlEncode(iv);
            Iv = iv;
        }
        
        /// <summary>
        /// 获取向量字节数组。
        /// </summary>
        public byte[] Iv { get; private set; }
        /// <summary>
        /// 获取向量字符串。
        /// </summary>
        public string IvString { get; private set; }
    }
}