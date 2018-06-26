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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.IO;

namespace LibrameCore.Extensions.Server.StaticPages
{
    /// <summary>
    /// 静态页服务器。
    /// </summary>
    public class StaticPageServer : AbstractServerExtensionService<StaticPageServer>, IStaticPageServer
    {
        /// <summary>
        /// 构造一个 <see cref="StaticPageServer"/> 实例。
        /// </summary>
        /// <param name="reader">给定的 <see cref="IStaticPageReader"/>。</param>
        /// <param name="writer">给定的 <see cref="IStaticPageWriter"/>。</param>
        /// <param name="options">给定的服务器选项。</param>
        public StaticPageServer(IStaticPageReader reader, IStaticPageWriter writer,
            IOptionsMonitor<ServerExtensionOptions> options)
            : base(options)
        {
            Reader = reader.NotNull(nameof(reader));
            Writer = writer.NotNull(nameof(writer));
        }


        /// <summary>
        /// 生成器。
        /// </summary>
        public IStaticPageReader Reader { get; }

        /// <summary>
        /// 写入器。
        /// </summary>
        public IStaticPageWriter Writer { get; }

        /// <summary>
        /// 启用此功能。
        /// </summary>
        public bool Enabled => Writer.Options.StaticPage.Enabled;


        /// <summary>
        /// 内容类型。
        /// </summary>
        public string ContentType { get; set; } = "text/html";


        /// <summary>
        /// 动作执行前。
        /// </summary>
        /// <param name="context">给定的 <see cref="ActionExecutingContext"/>。</param>
        public virtual void ActionExecuting(ActionExecutingContext context)
        {
            if (!Enabled) return;

            var routeInfo = context.RouteData.AsRouteInfo();
            var savePath = Writer.GetSavePath(routeInfo);
            
            // 如果静态文件已存在
            if (File.Exists(savePath))
            {
                using (var fs = File.Open(savePath, FileMode.Open))
                {
                    using (var sr = new StreamReader(fs, Writer.Encoding))
                    {
                        var result = new ContentResult();

                        result.Content = sr.ReadToEnd();
                        result.ContentType = ContentType;

                        context.Result = result;
                    }
                }
            }
        }


        /// <summary>
        /// 动作执行后。
        /// </summary>
        /// <param name="context">给定的 <see cref="ActionExecutedContext"/>。</param>
        public virtual void ActionExecuted(ActionExecutedContext context)
        {
            if (!Enabled) return;

            if (context.Result is ViewResult)
            {
                var routeInfo = context.RouteData.AsRouteInfo();

                // 取得呈现视图的内容
                var htmlCode = Reader.RenderToStringAsync(context)
                    .GetAwaiter().GetResult();

                // 可格式化内容
                htmlCode = FormatHtmlCode(htmlCode);

                // 建立静态页文件
                Writer.BuildAsync(htmlCode, routeInfo).GetAwaiter().GetResult();

                var result = new ContentResult();
                result.Content = htmlCode;
                result.ContentType = ContentType;

                context.Result = result;
            }
        }


        /// <summary>
        /// 格式化静态页内容。
        /// </summary>
        /// <param name="htmlCode">给定的 HTML 代码。</param>
        /// <returns>返回字符串。</returns>
        protected virtual string FormatHtmlCode(string htmlCode)
        {
            return htmlCode;
        }

    }
}
