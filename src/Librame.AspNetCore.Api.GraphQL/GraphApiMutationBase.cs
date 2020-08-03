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
    /// 图形 API 变化基类。
    /// </summary>
    public class GraphApiMutationBase : ObjectGraphType, IGraphApiMutation
    {
        /// <summary>
        /// 构造一个 <see cref="GraphApiMutationBase"/>。
        /// </summary>
        /// <param name="injectionService">给定的 <see cref="IInjectionService"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/></param>
        protected GraphApiMutationBase(IInjectionService injectionService,
            ILoggerFactory loggerFactory)
            : this(loggerFactory)
        {
            injectionService.NotNull(nameof(injectionService)).Inject(this);
        }

        /// <summary>
        /// 构造一个 <see cref="GraphApiMutationBase"/>。
        /// </summary>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/></param>
        protected GraphApiMutationBase(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory.NotNull(nameof(loggerFactory));
            Logger = loggerFactory.CreateLogger(GetType());

            Name = nameof(ISchema.Mutation);
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
