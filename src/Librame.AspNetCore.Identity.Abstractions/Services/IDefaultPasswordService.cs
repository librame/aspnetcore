#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Services
{
    using Extensions.Core.Services;

    /// <summary>
    /// 默认密码服务接口。
    /// </summary>
    public interface IDefaultPasswordService : IService
    {
        /// <summary>
        /// 获取默认密码。
        /// </summary>
        /// <param name="user">给定的用户。</param>
        /// <returns>返回字符串。</returns>
        string GetDefaultPassword(object user);
    }
}
