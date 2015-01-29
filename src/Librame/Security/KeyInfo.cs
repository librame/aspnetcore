// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Librame.Security
{
    /// <summary>
    /// 密钥信息。
    /// </summary>
    /// <author>Librame Pang</author>
    public class KeyInfo
    {
        /// <summary>
        /// 构造一个密钥信息实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// keyString 为空或 <see cref="String.Empty"/>。
        /// </exception>
        /// <param name="keyString">给定的密钥字符串。</param>
        public KeyInfo(string keyString)
        {
            if (String.IsNullOrEmpty(keyString))
            {
                throw new ArgumentNullException("keyString");
            }

            Key = WebEncoders.Base64UrlDecode(keyString);
            KeyString = keyString;
        }
        /// <summary>
        /// 构造一个密钥信息实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// key 为空。
        /// </exception>
        /// <param name="key">给定的密钥字节数组。</param>
        public KeyInfo(byte[] key)
        {
            if (ReferenceEquals(key, null))
            {
                throw new ArgumentNullException("key");
            }

            KeyString = WebEncoders.Base64UrlEncode(key);
            Key = key;
        }

        /// <summary>
        /// 获取密钥字节数组。
        /// </summary>
        public byte[] Key { get; private set; }

        /// <summary>
        /// 获取密钥字符串。
        /// </summary>
        public string KeyString { get; private set; }
    }
}