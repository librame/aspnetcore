#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL.Types;

namespace Librame.AspNetCore.Api
{
    using Extensions;

    /// <summary>
    /// 内部 Graph API 架构。
    /// </summary>
    internal class InternalGraphApiSchema : Schema, IGraphApiSchema
    {
        /// <summary>
        /// 构造一个 <see cref="InternalGraphApiSchema"/> 实例。
        /// </summary>
        /// <param name="mutation">给定的 <see cref="IGraphApiMutation"/>。</param>
        /// <param name="query">给定的 <see cref="IGraphApiQuery"/>。</param>
        /// <param name="subscription">给定的 <see cref="IGraphApiSubscription"/>。</param>
        public InternalGraphApiSchema(IGraphApiMutation mutation,
            IGraphApiQuery query, IGraphApiSubscription subscription)
        {
            Mutation = mutation.NotNull(nameof(mutation));
            Query = query.NotNull(nameof(query));
            Subscription = subscription.NotNull(nameof(subscription));
        }

    }
}
