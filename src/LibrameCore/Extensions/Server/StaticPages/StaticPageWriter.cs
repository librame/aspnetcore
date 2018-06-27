#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Extensions.Algorithm;
using LibrameStandard.Utilities;
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
        /// 默认文件扩展名。
        /// </summary>
        public const string DEFAULT_FILE_EXTENSION = ".html";


        /// <summary>
        /// 构造一个 <see cref="StaticPageWriter"/> 实例。
        /// </summary>
        /// <param name="hash">给定的 <see cref="IHashAlgorithm"/>。</param>
        /// <param name="options">给定的网络选项。</param>
        public StaticPageWriter(IHashAlgorithm hash, IOptionsMonitor<ServerExtensionOptions> options)
            : base(options)
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
        /// 保存路径。
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// 文件扩展名。
        /// </summary>
        public string FileExtension { get; set; } = DEFAULT_FILE_EXTENSION;


        /// <summary>
        /// 异步建立文件。
        /// </summary>
        /// <param name="content">给定的文件内容。</param>
        /// <param name="routeInfo">给定的路由信息。</param>
        /// <returns>返回一个异步操作。</returns>
        public async Task BuildAsync(string content, RouteInfo routeInfo)
        {
            if (string.IsNullOrEmpty(SavePath))
            {
                SavePath = GetSavePath(routeInfo.NotNull(nameof(routeInfo)));
            }

            using (var fs = File.Open(SavePath, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs, Encoding))
                {
                    if (Options.StaticPage.AppendTimestamp)
                        content += (Environment.NewLine + "<!-- " + typeof(StaticPageWriter).FullName + ": " + DateTime.Now.ToString() + " -->");

                    await sw.WriteAsync(content);
                }
            }
        }


        /// <summary>
        /// 得到保存路径。
        /// </summary>
        /// <param name="routeInfo">给定的路由信息。</param>
        /// <returns>返回字符串。</returns>
        public virtual string GetSavePath(RouteInfo routeInfo)
        {
            var directory = GenerateDirectory(routeInfo);
            var filename = GenerateFilename(routeInfo);

            return Path.Combine(directory, filename);
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
