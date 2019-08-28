#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections;

namespace Microsoft.AspNetCore.Razor.TagHelpers
{
    /// <summary>
    /// 多选标签助手。
    /// </summary>
    /// <remarks>
    /// 需搭配 bootstrap-multiselect 使用。
    /// <code>
    /// $(function(){
    ///     $("#multiselect").multiselect({
    ///         nonSelectedText: '请选择',
    ///         enableFiltering: true, // 是否显示过滤
    ///         filterPlaceholder: '查找',
    ///         includeSelectAllOption: true, // 是否显示全选
    ///         selectAllText: '全选',
    ///         numberDisplayed: {1} // 显示条数
    ///     });
    /// });
    /// </code>
    /// </remarks>
    [HtmlTargetElement("multiSelect", Attributes = ForAttributeName)]
    [HtmlTargetElement("multiSelect", Attributes = DataSourceAttributeName)]
    [HtmlTargetElement("multiSelect", Attributes = DataGroupAttributeName)]
    [HtmlTargetElement("multiSelect", Attributes = DataTextAttributeName)]
    [HtmlTargetElement("multiSelect", Attributes = DataValueAttributeName)]
    [HtmlTargetElement("multiSelect", Attributes = SelectedValuesAttributeName)]
    //[HtmlTargetElement("multiSelect", Attributes = ShowItemsCountAttributeName)]
    //[HtmlTargetElement("multiSelect", Attributes = ReadonlyAttributeName)]
    public class MultiSelectTagHelper : TagHelper
    {
        internal const string ForAttributeName = "asp-for";
        internal const string DataSourceAttributeName = "asp-dataSource";
        internal const string DataGroupAttributeName = "asp-dataGroup";
        internal const string DataTextAttributeName = "asp-dataText";
        internal const string DataValueAttributeName = "asp-dataValue";
        internal const string SelectedValuesAttributeName = "asp-selectedValues";
        //internal const string ShowItemsCountAttributeName = "asp-showItemsCount";
        //internal const string ReadonlyAttributeName = "asp-readonly";
        
        private readonly IHtmlGenerator _generator;


        /// <summary>
        /// 构造一个 <see cref="MultiSelectTagHelper"/> 实例。
        /// </summary>
        /// <param name="generator">给定的 <see cref="IHtmlGenerator"/>。</param>
        public MultiSelectTagHelper(IHtmlGenerator generator)
        {
            _generator = generator.NotNull(nameof(generator));
        }


        /// <summary>
        /// 视图上下文。
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// 为属性表达式。
        /// </summary>
        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }
        
        /// <summary>
        /// 数据源。
        /// </summary>
        [HtmlAttributeName(DataSourceAttributeName)]
        public IEnumerable DataSource { get; set; }

        /// <summary>
        /// 数据分组字段。
        /// </summary>
        [HtmlAttributeName(DataGroupAttributeName)]
        public string DataGroupField { get; set; }

        /// <summary>
        /// 数据文本字段。
        /// </summary>
        [HtmlAttributeName(DataTextAttributeName)]
        public string DataTextField { get; set; }

        /// <summary>
        /// 数据值字段。
        /// </summary>
        [HtmlAttributeName(DataValueAttributeName)]
        public string DataValueField { get; set; }

        /// <summary>
        /// 选中值集合。
        /// </summary>
        [HtmlAttributeName(SelectedValuesAttributeName)]
        public IEnumerable SelectedValues { get; set; }

        ///// <summary>
        ///// 显示项集合数量（默认为7）。
        ///// </summary>
        //[HtmlAttributeName(ShowItemsCountAttributeName)]
        //public int ShowItemsCount { get; set; } = 7;

        ///// <summary>
        ///// 只读。
        ///// </summary>
        //[HtmlAttributeName(ReadonlyAttributeName)]
        //public bool Readonly { get; set; }


        /// <summary>
        /// 处理上下文。
        /// </summary>
        /// <param name="context">给定的 <see cref="TagHelperContext"/>。</param>
        /// <param name="output">给定的 <see cref="TagHelperOutput"/>。</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            context.NotNull(nameof(context));
            output.NotNull(nameof(output));

            output.TagName = "div";
            //output.Attributes.Add("class", "multiselect-drop");

            var selectList = new MultiSelectList(DataSource, DataTextField, DataValueField, SelectedValues, DataGroupField);
            var builder = new HtmlContentBuilder();

            if (For.IsNull())
            {
                var id = output.Attributes["id"].Value.ToString();
                output.Attributes.Remove(output.Attributes["id"]);

                var options = string.Empty;
                foreach (var item in selectList)
                {
                    options += $"<option value=\"{item.Value}\" {(item.Selected ? "selected" : "")}>{item.Text}</option>";
                }

                builder.AppendHtml($"<select id=\"{id}\" name=\"{id}\" multiple=\"multiple\">{options}</select>");
            }
            else
            {
                var dropDown = _generator.GenerateSelect(ViewContext, For.ModelExplorer, For.Name, string.Empty, selectList, true, null);
                builder.AppendHtml(dropDown);
            }
            
            output.Content.AppendHtml(builder);

            base.Process(context, output);
        }

    }
}
