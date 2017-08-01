#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Utilities;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace LibrameCore.Filtration.SensitiveWord
{
    /// <summary>
    /// 文件型敏感词过滤器。
    /// </summary>
    public class FileSensitiveWordFilter : ISensitiveWordFilter
    {
        /// <summary>
        /// 构造一个文件型敏感词过滤器实例。
        /// </summary>
        /// <param name="options">给定的过滤选项。</param>
        public FileSensitiveWordFilter(IOptions<FiltrationOptions> options)
        {
            Options = options.NotNull(nameof(options)).Value;

            var words = Options.SensitiveWord.Words.AsOrDefault(SensitiveWordOptions.DefaultWords);
            Words = words.Split(',');
        }


        /// <summary>
        /// 过滤选项。
        /// </summary>
        public FiltrationOptions Options { get; }
        
        /// <summary>
        /// 单词数组。
        /// </summary>
        public string[] Words { get; }


        /// <summary>
        /// 过滤内容。
        /// </summary>
        /// <param name="content">给定的要过滤的内容。</param>
        /// <param name="replacement">用于替换的字符串（可选；默认使用选项设定）。</param>
        /// <returns>返回是否包含敏感词、过滤后（如果包含）的字符串、替换的字典信息。</returns>
        public virtual (bool exists, string content, Dictionary<string, int> replaces) Filting(string content, string replacement = null)
        {
            if (string.IsNullOrEmpty(content) || !Options.SensitiveWord.Enabled)
                return (false, content, null);

            replacement = replacement.AsOrDefault(Options.SensitiveWord.Replacement);
            
            var exists = false;
            var replaces = new Dictionary<string, int>();

            foreach (var w in Words)
            {
                // 如果已过滤为空字符串，则跳出
                if (string.IsNullOrEmpty(content))
                    break;
                
                // 如果包含敏感词
                if (content.Contains(w))
                {
                    if (!exists)
                        exists = true;

                    if (replaces.ContainsKey(w))
                    {
                        var count = replaces[w];
                        replaces[w] = count++;
                    }
                    else
                    {
                        replaces.Add(w, 1);
                    }
                    
                    content = content.Replace(w, replacement);
                }
            }

            return (exists, content, replaces);
        }

    }
}
