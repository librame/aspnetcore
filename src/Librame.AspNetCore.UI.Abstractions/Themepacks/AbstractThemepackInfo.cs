#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.FileProviders;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 抽象主题包信息。
    /// </summary>
    public abstract class AbstractThemepackInfo : AbstractApplicationInfo, IThemepackInfo
    {
        /// <summary>
        /// 获取静态文件提供程序。
        /// </summary>
        /// <returns>返回 <see cref="IFileProvider"/>。</returns>
        public abstract IFileProvider GetStaticFileProvider();
    }
}
