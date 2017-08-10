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
using System.Text;
using System.Threading.Tasks;

namespace LibrameCore.Filtration.StaticalHtml
{
    /// <summary>
    /// HTML 构建器接口。
    /// </summary>
    public interface IHtmlBuilder
    {
        /// <summary>
        /// 算法选项。
        /// </summary>
        AlgorithmOptions AlgoOptions { get; }

        /// <summary>
        /// 过滤选项。
        /// </summary>
        FiltrationOptions FiltOptions { get; }


        /// <summary>
        /// HTML 文件扩展名。
        /// </summary>
        string FileExtension { get; }


        /// <summary>
        /// 异步建立 HTML 文件。
        /// </summary>
        /// <param name="content">给定的文件内容。</param>
        /// <param name="routeInfo">给定的路由信息。</param>
        /// <param name="encoding">给定的字符编码（可选；默认为 UTF8）。</param>
        /// <returns>返回一个异步操作。</returns>
        Task<string> BuildAsync(string content, RouteInfo routeInfo, Encoding encoding = null);


        /// <summary>
        /// 建立完整文件名。
        /// </summary>
        /// <param name="routeInfo">给定的路由信息。</param>
        /// <returns>返回字符串。</returns>
        string BuildFullFilename(RouteInfo routeInfo);
    }
}
