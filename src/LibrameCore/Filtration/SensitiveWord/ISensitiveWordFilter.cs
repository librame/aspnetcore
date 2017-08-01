#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;

namespace LibrameCore.Filtration.SensitiveWord
{
    /// <summary>
    /// 敏感词过滤器接口。
    /// </summary>
    public interface ISensitiveWordFilter
    {
        /// <summary>
        /// 过滤选项。
        /// </summary>
        FiltrationOptions Options { get; }


        /// <summary>
        /// 单词数组。
        /// </summary>
        string[] Words { get; }


        /// <summary>
        /// 过滤内容。
        /// </summary>
        /// <param name="content">给定的要过滤的内容。</param>
        /// <param name="replacement">用于更换的字符串（可选）。</param>
        /// <returns>返回是否包含敏感词、过滤后（如果包含）的字符串、替换的字典信息。</returns>
        (bool exists, string content, Dictionary<string, int> replaces) Filting(string content, string replacement = null);
    }
}
