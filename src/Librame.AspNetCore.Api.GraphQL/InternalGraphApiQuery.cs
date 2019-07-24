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
    /// 内部图形 API 查询。
    /// </summary>
    internal class InternalGraphApiQuery : ObjectGraphType, IGraphApiQuery
    {
        /// <summary>
        /// 构造一个 <see cref="InternalGraphApiQuery"/> 实例。
        /// </summary>
        public InternalGraphApiQuery()
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
