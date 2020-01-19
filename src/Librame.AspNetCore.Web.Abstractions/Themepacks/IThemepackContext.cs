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

namespace Librame.AspNetCore.Web.Themepacks
{
    using Builders;
    using Extensions.Core.Services;

    /// <summary>
    /// 主题包上下文接口。
    /// </summary>
    public interface IThemepackContext
    {
        /// <summary>
        /// 服务工厂。
        /// </summary>
        ServiceFactory ServiceFactory { get; }

        /// <summary>
        /// Web 构建器。
        /// </summary>
        IWebBuilder Builder { get; }

        /// <summary>
        /// 主题包信息字典集合。
        /// </summary>
        IReadOnlyDictionary<string, IThemepackInfo> Infos { get; }


        /// <summary>
        /// 当前主题包信息。
        /// </summary>
        IThemepackInfo CurrentInfo { get; set; }
    }
}
