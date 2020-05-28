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
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    class GraphApiQuery : ObjectGraphType, IGraphApiQuery
    {
        public GraphApiQuery()
        {
            Name = nameof(ISchema.Query);

            Field<StringGraphType>
            (
                name: "hello",
                resolve: context => "Librame"
            );
        }

    }
}
