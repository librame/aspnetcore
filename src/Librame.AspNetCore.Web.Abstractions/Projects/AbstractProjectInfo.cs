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
    using Extensions;
    using Extensions.Core.Services;

    /// <summary>
    /// 抽象项目信息。
    /// </summary>
    public abstract class AbstractProjectInfo : AbstractApplicationInfo, IProjectInfo
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractProjectInfo"/>。
        /// </summary>
        /// <param name="serviceFactory">给定的 <see cref="Extensions.Core.Services.ServiceFactory"/>。</param>
        protected AbstractProjectInfo(ServiceFactory serviceFactory)
            : base()
        {
            ServiceFactory = serviceFactory.NotNull(nameof(serviceFactory));
        }


        /// <summary>
        /// 服务工厂。
        /// </summary>
        public ServiceFactory ServiceFactory { get; }
    }
}
