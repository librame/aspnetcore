#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Text;

namespace Librame.Utility
{
    /// <summary>
    /// <see cref="Encoding"/> 实用工具。
    /// </summary>
    public static class EncodingUtil
    {
        /// <summary>
        /// 将代码页名称转换为关联的字符编码。
        /// </summary>
        /// <param name="name">首选编码的代码页名称。</param>
        /// <returns>返回与指定代码页关联的编码。</returns>
        public static Encoding AsEncoding(this string name)
        {
            return Encoding.GetEncoding(name.NotEmpty(nameof(name)));
        }

        /// <summary>
        /// 将字符编码转换为可配置的代码页名称。
        /// </summary>
        /// <param name="encoding">给定的字符编码。</param>
        /// <returns>返回代码页名称。</returns>
        public static string AsName(this Encoding encoding)
        {
            return encoding.NotNull(nameof(encoding)).EncodingName;
        }

    }
}
