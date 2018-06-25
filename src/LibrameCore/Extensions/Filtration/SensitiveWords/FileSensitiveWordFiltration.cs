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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace LibrameCore.Extensions.Filtration.SensitiveWords
{
    /// <summary>
    /// 文件敏感词过滤。
    /// </summary>
    public class FileSensitiveWordFiltration : AbstractFiltrationExtensionService<FileSensitiveWordFiltration>, ISensitiveWordFiltration
    {
        /// <summary>
        /// 构造一个文件型敏感词过滤器实例。
        /// </summary>
        /// <param name="options">给定的过滤选项。</param>
        /// <param name="logger">给定的记录器。</param>
        public FileSensitiveWordFiltration(IOptions<FiltrationExtensionOptions> options, ILogger<FileSensitiveWordFiltration> logger)
            : base(options, logger)
        {
        }

        
        /// <summary>
        /// 单词数组。
        /// </summary>
        public virtual string[] Words
        {
            get
            {
                if (string.IsNullOrEmpty(Options.SensitiveWord.Words))
                    return null;

                return Options.SensitiveWord.Words.Split(',');
            }
        }


        /// <summary>
        /// 过滤动作执行前上下文参数集合。
        /// </summary>
        /// <param name="context">给定的 <see cref="ActionExecutingContext"/>。</param>
        public virtual void Filting(ActionExecutingContext context)
        {
            if (!Options.SensitiveWord.Enabled)
                return;

            foreach (var para in context.ActionDescriptor.Parameters)
            {
                switch (para.ParameterType.FullName)
                {
                    case "System.String":
                        {
                            var arg = context.ActionArguments[para.Name];

                            if (arg != null)
                            {
                                var result = Filting(arg.ToString());

                                if (result.Exists)
                                    context.ActionArguments[para.Name] = result.Content;
                            }
                        }
                        break;

                    case "Microsoft.AspNetCore.Http.IFormCollection":
                        {
                            var form = context.ActionArguments[para.Name] as IFormCollection;

                            if (form != null)
                            {
                                var fields = new Dictionary<string, StringValues>();

                                foreach (var key in form.Keys)
                                {
                                    var value = form[key];
                                    var result = Filting(value.ToString());

                                    if (result.Exists)
                                        fields.Add(key, new StringValues(result.Content));
                                    else
                                        fields.Add(key, value);
                                }

                                context.ActionArguments[para.Name] = new FormCollection(fields);
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
        }


        /// <summary>
        /// 过滤内容。
        /// </summary>
        /// <param name="content">给定的要过滤的内容。</param>
        /// <param name="replacement">用于替换的字符串（可选；默认使用选项设定）。</param>
        /// <returns>返回是否包含敏感词、过滤后（如果包含）的字符串、替换的字典信息。</returns>
        public virtual (bool Exists, string Content, Dictionary<string, int> Replaces) Filting(string content, string replacement = null)
        {
            if (string.IsNullOrEmpty(content) || !Options.SensitiveWord.Enabled || Words == null || Words.Length < 1)
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
