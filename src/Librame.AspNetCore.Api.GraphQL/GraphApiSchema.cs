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
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Api
{
    using Extensions;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
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
