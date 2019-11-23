﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL.Types;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Api
{
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    class GraphApiMutation : ObjectGraphType, IGraphApiMutation
    {
        public GraphApiMutation()
        {
            Name = nameof(ISchema.Mutation);

            Field<StringGraphType>
            (
                name: "hello",
                resolve: context => "Librame"
            );
        }

    }
}
