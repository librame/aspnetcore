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

    class GraphApiSchema : Schema, IGraphApiSchema
    {
        public GraphApiSchema(IGraphApiMutation mutation,
            IGraphApiQuery query, IGraphApiSubscription subscription)
        {
            Mutation = mutation.NotNull(nameof(mutation));
            Query = query.NotNull(nameof(query));
            Subscription = subscription.NotNull(nameof(subscription));
        }

    }
}
