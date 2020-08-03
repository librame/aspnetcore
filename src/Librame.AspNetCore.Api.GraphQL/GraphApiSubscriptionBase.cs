#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL.Types;
using Microsoft.Extensions.Logging;

namespace Librame.AspNetCore.Api
{
    using Extensions;
    using Extensions.Core.Services;

    /// <summary>
    /// 图形 API 订阅基类。
    /// </summary>
    public class GraphApiSubscriptionBase : ObjectGraphType, IGraphApiSubscription
    {
        /// <summary>
        /// 构造一个 <see cref="GraphApiSubscriptionBase"/>。
        /// </summary>
        /// <param name="injectionService">给定的 <see cref="IInjectionService"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/></param>
        protected GraphApiSubscriptionBase(IInjectionService injectionService,
            ILoggerFactory loggerFactory)
            : this(loggerFactory)
        {
            injectionService.NotNull(nameof(injectionService)).Inject(this);
        }

        /// <summary>
        /// 构造一个 <see cref="GraphApiSubscriptionBase"/>。
        /// </summary>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/></param>
        protected GraphApiSubscriptionBase(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory.NotNull(nameof(loggerFactory));
            Logger = loggerFactory.CreateLogger(GetType());

            Name = nameof(ISchema.Subscription);
        }


        /// <summary>
        /// 日志工厂。
        /// </summary>
        public ILoggerFactory LoggerFactory { get; }

        /// <summary>
        /// 日志。
        /// </summary>
        protected ILogger Logger { get; }
    }
}
