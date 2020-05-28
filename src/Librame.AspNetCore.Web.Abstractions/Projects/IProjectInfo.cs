#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Web.Projects
{
    using AspNetCore.Applications;
    using Extensions.Core.Services;

    /// <summary>
    /// 项目信息接口。
    /// </summary>
    public interface IProjectInfo : IApplicationInfo
    {
        /// <summary>
        /// 服务工厂。
        /// </summary>
        ServiceFactory ServiceFactory { get; }
    }
}
