#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 抽象界面配置。
    /// </summary>
    public abstract class AbstractInterfaceConfiguration : IInterfaceConfiguration
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractInterfaceConfiguration"/>。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        protected AbstractInterfaceConfiguration(IApplicationContext context)
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
