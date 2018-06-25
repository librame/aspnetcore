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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;

namespace LibrameCore.Extensions.Server.StaticPages
{
    /// <summary>
    /// 静态页服务器。
    /// </summary>
    public class StaticPageServer : AbstractServerExtensionService<StaticPageServer>, IStaticPageServer
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractServerExtensionService{TServer}"/> 实例。
        /// </summary>
        /// <param name="generator">给定的生成器。</param>
        /// <param name="writer">给定的写入器。</param>
        /// <param name="options">给定的服务器选项。</param>
        /// <param name="logger">给定的记录器。</param>
        public StaticPageServer(IStaticPageGenerator generator, IStaticPageWriter writer,
            IOptions<ServerExtensionOptions> options, ILogger<StaticPageServer> logger)
            : base(options, logger)
        {
            Generator = generator.NotNull(nameof(generator));
            Writer = writer.NotNull(nameof(writer));
        }


        /// <summary>
        /// 生成器。
        /// </summary>
        public IStaticPageGenerator Generator { get; }

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
            if (!Enabled)
                return;

            var routeInfo = context.RouteData.AsRouteInfo();
            var filename = Writer.BuildFullFilename(routeInfo);
            
            // 如果静态文件已存在
            if (File.Exists(filename))
            {
                try
                {
                    using (var fs = File.Open(filename, FileMode.Open))
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
                catch (Exception ex)
                {
                    Logger.LogError(ex.Message);
                }
            }
        }


        /// <summary>
        /// 动作执行后。
        /// </summary>
        /// <param name="context">给定的 <see cref="ActionExecutedContext"/>。</param>
        public virtual void ActionExecuted(ActionExecutedContext context)
        {
            if (!Enabled)
                return;

            if (context.Result is ViewResult)
            {
                var routeInfo = context.RouteData.AsRouteInfo();

                // 取得呈现视图的内容
                var sb = Generator.RenderAsync(context)
                    .GetAwaiter().GetResult();

                // 可格式化内容
                var content = FormatContent(sb);

                // 建立静态页文件
                var filename = Writer.BuildAsync(content, routeInfo)
                    .GetAwaiter().GetResult();

                var result = new ContentResult();
                result.Content = content;
                result.ContentType = ContentType;

                context.Result = result;
            }
        }


        /// <summary>
        /// 格式化静态页内容。
        /// </summary>
        /// <param name="builder">给定的字符串构建器。</param>
        /// <returns>返回字符串。</returns>
        protected virtual string FormatContent(StringBuilder builder)
        {
            return builder.ToString();
        }

    }
}
