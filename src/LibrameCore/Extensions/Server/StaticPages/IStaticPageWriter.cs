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
using System.Text;
using System.Threading.Tasks;

namespace LibrameCore.Extensions.Server
{
    /// <summary>
    /// 静态页写入器接口。
    /// </summary>
    public interface IStaticPageWriter : IServerExtensionService
    {
        /// <summary>
        /// 哈希算法。
        /// </summary>
        IHashAlgorithm Hash { get; }

        /// <summary>
        /// 字符编码。
        /// </summary>
        Encoding Encoding { get; set; }


        /// <summary>
        /// 保存路径。
        /// </summary>
        string SavePath { get; set; }
        
        /// <summary>
        /// 文件扩展名。
        /// </summary>
        string FileExtension { get; set; }


        /// <summary>
        /// 异步建立文件。
        /// </summary>
        /// <param name="content">给定的文件内容。</param>
        /// <param name="routeInfo">给定的路由信息。</param>
        /// <returns>返回一个异步操作。</returns>
        Task BuildAsync(string content, RouteInfo routeInfo);


        /// <summary>
        /// 得到保存路径。
        /// </summary>
        /// <param name="routeInfo">给定的路由信息。</param>
        /// <returns>返回字符串。</returns>
        string GetSavePath(RouteInfo routeInfo);
    }
}
