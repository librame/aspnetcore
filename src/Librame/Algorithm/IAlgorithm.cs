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
using System.Text;

namespace Librame.Algorithm
{
    using TextCodecs;

    /// <summary>
    /// 算法接口。
    /// </summary>
    public interface IAlgorithm
    {
        /// <summary>
        /// 选项项。
        /// </summary>
        LibrameOptions Options { get; }

        /// <summary>
        /// 记录器。
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// 字符编码。
        /// </summary>
        Encoding Encoding { get; set; }


        /// <summary>
        /// 密文编解码器。
        /// </summary>
        ITextCodec Cipher { get; }

        /// <summary>
        /// 明文编解码器。
        /// </summary>
        ITextCodec Plain { get; }
    }
}
