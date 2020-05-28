#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Web.Services
{
    using Extensions.Core.Services;

    /// <summary>
    /// 版权声明服务接口。
    /// </summary>
    public interface ICopyrightService : IService
    {
        /// <summary>
        /// 获取 HTML 代码。
        /// </summary>
        /// <param name="displayMiniInfo">是否显示迷你信息（可选；默认不显示迷你信息）。</param>
        /// <returns>返回字符串。</returns>
        string GetHtmlCode(bool displayMiniInfo = false);
    }
}
