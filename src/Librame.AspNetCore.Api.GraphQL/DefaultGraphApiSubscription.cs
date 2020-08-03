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
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Api
{
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class DefaultGraphApiSubscription : GraphApiSubscriptionBase
    {
        public DefaultGraphApiSubscription(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            Field<StringGraphType>
            (
                name: "hello",
                resolve: context => "Welcome to use graph api subscription."
            );
        }

    }
}
