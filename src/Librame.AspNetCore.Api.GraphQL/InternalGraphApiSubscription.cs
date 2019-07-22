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
    /// <summary>
    /// 内部 Graph API 订阅。
    /// </summary>
    internal class InternalGraphApiSubscription : ObjectGraphType, IGraphApiSubscription
    {
        /// <summary>
        /// 构造一个 <see cref="InternalGraphApiSubscription"/> 实例。
        /// </summary>
        public InternalGraphApiSubscription()
        {
            Name = nameof(ISchema.Subscription);
        }
    }
}
