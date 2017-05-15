#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Algorithm
{
    using Utility;

    /// <summary>
    /// BASE64 字节转换器。
    /// </summary>
    public class Base64ByteConverter : IByteConverter
    {
        /// <summary>
        /// 字节数组转换器。
        /// </summary>
        /// <param name="buffer">给定的字节数组。</param>
        /// <returns>返回字符串。</returns>
        public string ToString(byte[] buffer)
        {
            return buffer.AsBase64();
        }

        /// <summary>
        /// 将字符串还原为字节数组。
        /// </summary>
        /// <param name="encrypt">给定的字符串。</param>
        /// <returns>返回字节数组。</returns>
        public byte[] FromString(string encrypt)
        {
            return encrypt.FromBase64();
        }

    }
}
