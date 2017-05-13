#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Text;

namespace Librame.Utility
{
    /// <summary>
    /// <see cref="Byte"/> 实用工具。
    /// </summary>
    public static class ByteUtil
    {
        /// <summary>
        /// 转换字节序列为 BASE64。
        /// </summary>
        /// <param name="buffer">给定的字节数组。</param>
        /// <returns>返回 BASE64 字符串。</returns>
        public static string ToBase64(this byte[] buffer)
        {
            return Convert.ToBase64String(buffer.NotNull(nameof(buffer)));
        }

        /// <summary>
        /// 还原 BASE64 为字节序列。
        /// </summary>
        /// <param name="base64">给定的 BASE64 字符串。</param>
        /// <returns>返回字节数组。</returns>
        public static byte[] FromBase64(this string base64)
        {
            return Convert.FromBase64String(base64.NotNullOrEmpty(nameof(base64)));
        }


        /// <summary>
        /// 转换字节序列为十六进制。
        /// </summary>
        /// <param name="buffer">给定的字节数组。</param>
        /// <returns>返回十六进制字符串。</returns>
        public static string ToHex(this byte[] buffer)
        {
            // 同 BitConverter.ToString(buffer).Replace("-", string.Empty);

            var sb = new StringBuilder();

            if (buffer != null || buffer.Length == 0)
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    sb.Append(buffer[i].ToString("X2"));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 还原十六进制为字节序列。
        /// </summary>
        /// <param name="hex">给定的十六进制字符串。</param>
        /// <returns>返回字节数组。</returns>
        public static byte[] FromHex(this string hex)
        {
            if (string.IsNullOrEmpty(hex) || (hex.Length % 2 != 0))
                return null;

            int length = hex.Length / 2;
            var buffer = new byte[length];

            for (int i = 0; i < length; i++)
            {
                buffer[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }

            return buffer;
        }

    }
}
