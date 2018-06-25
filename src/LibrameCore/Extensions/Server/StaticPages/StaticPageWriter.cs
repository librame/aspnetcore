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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LibrameCore.Extensions.Server.StaticPages
{
    /// <summary>
    /// 静态页写入器。
    /// </summary>
    public class StaticPageWriter : AbstractServerExtensionService<StaticPageWriter>, IStaticPageWriter
    {

        /// <summary>
        /// 构造一个 <see cref="StaticPageWriter"/> 实例。
        /// </summary>
        /// <param name="hash">给定的哈希算法。</param>
        /// <param name="options">给定的网络选项。</param>
        /// <param name="logger">给定的记录器。</param>
        public StaticPageWriter(IHashAlgorithm hash,
            IOptions<ServerExtensionOptions> options, ILogger<StaticPageWriter> logger)
            : base(options, logger)
        {
            Hash = hash.NotNull(nameof(hash));
        }


        /// <summary>
        /// 哈希算法。
        /// </summary>
        public IHashAlgorithm Hash { get; }


        private Encoding _encoding;
        /// <summary>
        /// 字符编码。
        /// </summary>
        public Encoding Encoding
        {
            get
            {
                if (_encoding == null)
                    _encoding = Hash.Options.Encoding.AsEncoding();

                return _encoding;
            }
            set
            {
                _encoding = value.NotNull(nameof(value));
            }
        }


        /// <summary>
        /// 文件扩展名。
        /// </summary>
        public string FileExtension { get; set; } = ".html";


        /// <summary>
        /// 异步建立文件。
        /// </summary>
        /// <param name="content">给定的文件内容。</param>
        /// <param name="routeInfo">给定的路由信息。</param>
        /// <returns>返回一个异步操作。</returns>
        public async Task<string> BuildAsync(string content, RouteInfo routeInfo)
        {
            routeInfo.NotNull(nameof(routeInfo));
            
            var directory = GenerateDirectory(routeInfo);
            var filename = GenerateFilename(routeInfo);

            filename = Path.Combine(directory, filename);

            using (var fs = File.Open(filename, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs, Encoding))
                {
                    if (Options.StaticPage.AppendTimestamp)
                        content += (Environment.NewLine + "<!-- " + typeof(StaticPageWriter).FullName + ": " + DateTime.Now.ToString() + " -->");

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
            try
            {
                var directory = GenerateDirectory(routeInfo);
                var filename = GenerateFilename(routeInfo);

                return Path.Combine(directory, filename);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);

                throw ex;
            }
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
                Options.StaticPage.FolderName);

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

            var filename = Options.StaticPage.FileNameFormat;

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
