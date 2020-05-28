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
    using AspNetCore.Web.Applications;
    using Extensions;

    /// <summary>
    /// 抽象项目配置。
    /// </summary>
    public abstract class AbstractProjectConfiguration : IProjectConfiguration
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractProjectConfiguration"/>。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        protected AbstractProjectConfiguration(IApplicationContext context)
        {
            Context = context.NotNull(nameof(context));
        }


        /// <summary>
        /// 应用上下文。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IApplicationContext"/>。
        /// </value>
        public IApplicationContext Context { get; }
    }
}
