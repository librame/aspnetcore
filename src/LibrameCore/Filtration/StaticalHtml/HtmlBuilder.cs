#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Algorithm;
using LibrameStandard.Utilities;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LibrameCore.Filtration.StaticalHtml
{
    /// <summary>
    /// HTML 构建器。
    /// </summary>
    public class HtmlBuilder : IHtmlBuilder
    {

        /// <summary>
        /// 构造一个 HTML 构建器实例。
        /// </summary>
        /// <param name="algorithmOptions">给定的算法选项。</param>
        /// <param name="filtrationOptions">给定的过滤选项。</param>
        public HtmlBuilder(IOptions<AlgorithmOptions> algorithmOptions,
            IOptions<FiltrationOptions> filtrationOptions)
        {
            AlgoOptions = algorithmOptions.NotNull(nameof(algorithmOptions)).Value;
            FiltOptions = filtrationOptions.NotNull(nameof(filtrationOptions)).Value;
        }


        /// <summary>
        /// 算法选项。
        /// </summary>
        public AlgorithmOptions AlgoOptions { get; }

        /// <summary>
        /// 过滤选项。
        /// </summary>
        public FiltrationOptions FiltOptions { get; }


        /// <summary>
        /// HTML 文件扩展名。
        /// </summary>
        public virtual string FileExtension => ".html";


        /// <summary>
        /// 异步建立 HTML 文件。
        /// </summary>
        /// <param name="content">给定的文件内容。</param>
        /// <param name="routeInfo">给定的路由信息。</param>
        /// <param name="encoding">给定的字符编码（可选；默认为 UTF8）。</param>
        /// <returns>返回一个异步操作。</returns>
        public async Task<string> BuildAsync(string content, RouteInfo routeInfo, Encoding encoding = null)
        {
            routeInfo.NotNull(nameof(routeInfo));
            
            encoding = encoding.AsOrDefault(AlgoOptions.Encoding.AsEncoding());

            var directory = GenerateDirectory(routeInfo);
            var filename = GenerateFilename(routeInfo);

            filename = directory.AppendPath(filename);

            using (var fs = File.Open(filename, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs, encoding))
                {
                    if (FiltOptions.StaticalHtml.AppendTimestamp)
                        content += (Environment.NewLine + "<!-- " + DateTime.Now.ToString() + " -->");

                    await sw.WriteAsync(content);
                }
            }

            return filename;
        }


        /// <summary>
        /// 建立完整文件名。
        /// </summary>
        /// <param name="routeInfo">给定的路由信息。</param>
        /// <returns>返回字符串。</returns>
        public virtual string BuildFullFilename(RouteInfo routeInfo)
        {
            var directory = GenerateDirectory(routeInfo);
            var filename = GenerateFilename(routeInfo);

            return directory.AppendPath(filename);
        }


        /// <summary>
        /// 生成目录。
        /// </summary>
        /// <param name="routeInfo">给定的路由信息。</param>
        /// <returns>返回目录字符串。</returns>
        protected virtual string GenerateDirectory(RouteInfo routeInfo)
        {
            var dir = Path.Combine(PathUtility.BaseDirectory,
                "wwwroot",
                FiltOptions.StaticalHtml.FolderName);

            if (!string.IsNullOrEmpty(routeInfo.Area))
                dir = Path.Combine(dir, routeInfo.Area);

            return dir.CreateDirectory()?.FullName;
        }
        
        /// <summary>
        /// 生成文件名。
        /// </summary>
        /// <param name="routeInfo">给定的路由信息。</param>
        /// <returns>返回文件名字符串。</returns>
        protected virtual string GenerateFilename(RouteInfo routeInfo)
        {
            var id = (string.IsNullOrEmpty(routeInfo.Id)
                ? string.Empty : ("-" + routeInfo.Id.ToLower()));

            var filename = FiltOptions.StaticalHtml.FileNameFormat;

            if (!string.IsNullOrEmpty(filename))
            {
                filename = filename.Replace("{controller}", routeInfo.Controller.ToLower());
                filename = filename.Replace("{action}", routeInfo.Action.ToLower());
                filename = filename.Replace("{id}", id);
                filename = filename.Replace("{extension}", FileExtension);
            }

            return filename;
        }

    }
}
