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
    /// 十六进制字节转换器。
    /// </summary>
    public class HexByteConverter : IByteConverter
    {
        /// <summary>
        /// 字节数组转换器。
        /// </summary>
        /// <param name="buffer">给定的字节数组。</param>
        /// <returns>返回字符串。</returns>
        public string AsString(byte[] buffer)
        {
            return buffer.AsHex();
        }

        /// <summary>
        /// 将字符串还原为字节数组。
        /// </summary>
        /// <param name="encrypt">给定的字符串。</param>
        /// <returns>返回字节数组。</returns>
        public byte[] FromString(string encrypt)
        {
            return encrypt.FromHex();
        }

    }
}
