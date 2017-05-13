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

namespace Librame.Templates
{
    using Utility;

    /// <summary>
    /// 模板格式化器接口。
    /// </summary>
    public interface ITemplateFormatter
    {
        /// <summary>
        /// 模板内容。
        /// </summary>
        string Template { get; }


        /// <summary>
        /// 建立可格式化键名。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回可格式化键名字符串。</returns>
        string BuildFormatKey(string name);
        

        /// <summary>
        /// 是否包含指定可格式化键名。
        /// </summary>
        /// <param name="formatKey">给定的可格式化键名。</param>
        /// <returns>返回是否包含的布尔值。</returns>
        bool ContainsFormatKey(string formatKey);


        /// <summary>
        /// 格式化模板内容。
        /// </summary>
        /// <param name="formatDictionary">给定要格式化的字典集合。</param>
        /// <returns>返回经过格式化的模板内容。</returns>
        string Formatting(IDictionary<string, string> formatDictionary);

        /// <summary>
        /// 格式化模板内容。
        /// </summary>
        /// <param name="formatKey">给定的可格式化键名。</param>
        /// <param name="value">用于格式化的值。</param>
        /// <returns>返回经过格式化的模板内容。</returns>
        string Formatting(string formatKey, string value);
    }


    /// <summary>
    /// 模板格式化器。
    /// </summary>
    public class TemplateFormatter : ITemplateFormatter
    {
        /// <summary>
        /// 构造一个适配器模板格式化程序。
        /// </summary>
        /// <param name="template">给定的模板内容。</param>
        public TemplateFormatter(string template)
        {
            Template = template.NotNullOrEmpty(nameof(template));
        }


        /// <summary>
        /// 模板内容。
        /// </summary>
        public string Template { get; }


        /// <summary>
        /// 建立可格式化键名。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回可格式化键名字符串。</returns>
        public virtual string BuildFormatKey(string name)
        {
            name.NotNullOrEmpty(nameof(name));

            // 如果不是可格式化键名（以花括号为规则）
            if (!name.StartsWith("{") && !name.EndsWith("}"))
                return ("{" + name + "}");
            
            return name;
        }

        
        /// <summary>
        /// 是否包含指定可格式化键名。
        /// </summary>
        /// <param name="formatKey">给定的可格式化键名。</param>
        /// <returns>返回布尔值。</returns>
        public virtual bool ContainsFormatKey(string formatKey)
        {
            return Template.Contains(formatKey.NotNullOrEmpty(nameof(formatKey)));
        }


        /// <summary>
        /// 格式化模板内容。
        /// </summary>
        /// <param name="formatDictionary">给定要格式化的字典集合。</param>
        /// <returns>返回经过格式化的模板内容。</returns>
        public virtual string Formatting(IDictionary<string, string> formatDictionary)
        {
            formatDictionary.NotNull(nameof(formatDictionary));

            var template = Template;

            formatDictionary.Invoke(pair =>
            {
                template = FormattingCore(pair.Key, pair.Value, template);
            });

            return template;
        }

        /// <summary>
        /// 格式化模板内容。
        /// </summary>
        /// <param name="formatKey">给定的可格式化键名。</param>
        /// <param name="formatValue">用于格式化的值。</param>
        /// <returns>返回经过格式化的模板内容。</returns>
        public virtual string Formatting(string formatKey, string formatValue)
        {
            return FormattingCore(formatKey, formatValue, Template);
        }

        /// <summary>
        /// 格式化模板内容。
        /// </summary>
        /// <param name="formatKey">给定的可格式化键名。</param>
        /// <param name="formatValue">用于格式化的值。</param>
        /// <param name="template">要格式化的模板内容。</param>
        /// <returns>返回经过格式化的模板内容。</returns>
        protected virtual string FormattingCore(string formatKey, string formatValue, string template)
        {
            formatKey.NotNullOrEmpty(nameof(formatKey));
            formatValue.NotNull(nameof(formatValue));

            if (string.IsNullOrEmpty(template))
                return template;

            // 转义可格式化键名
            var escapedFormatKey = EscapeFormatKey(formatKey);

            // 开始替换
            return template.Replace(escapedFormatKey, formatValue);
        }


        #region Escape

        /// <summary>
        /// 是否为转义可格式化键名。
        /// </summary>
        /// <param name="formatKey">给定待检测的转义可格式化键名。</param>
        /// <returns>返回布尔值。</returns>
        protected virtual bool IsEscapedFormatKey(string formatKey)
        {
            formatKey.NotNullOrEmpty(nameof(formatKey));

            return formatKey.StartsWith("{{{") && formatKey.EndsWith("}}}");
        }


        /// <summary>
        /// 转义可格式化键名。
        /// </summary>
        /// <param name="formatKey">给定的可格式化键名。</param>
        /// <returns>返回转义后的可格式化键名。</returns>
        protected virtual string EscapeFormatKey(string formatKey)
        {
            // 如果已是转义键名，则直接返回
            if (IsEscapedFormatKey(formatKey))
                return formatKey;

            // 转义花括号
            return ("{{" + formatKey + "}}");
        }


        /// <summary>
        /// 反转义可格式化键名。
        /// </summary>
        /// <param name="escapedFormatKey">给定经过转义的可格式化键名。</param>
        /// <returns>返回可格式化键名。</returns>
        protected virtual string UnescapeFormatKey(string escapedFormatKey)
        {
            // 如果不是转义键名，则直接返回
            if (!IsEscapedFormatKey(escapedFormatKey))
                return escapedFormatKey;

            // 移除首尾两组花括号
            return escapedFormatKey.Substring(2, escapedFormatKey.Length - 2);
        }

        #endregion

    }
}
