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
    /// 内部 API 查询。
    /// </summary>
    internal class InternalApiQuery : ObjectGraphType, IApiQuery
    {
        /// <summary>
        /// 构造一个 <see cref="InternalApiQuery"/> 实例。
        /// </summary>
        public InternalApiQuery()
        {
            Field<StringGraphType>
            (
                name: "hello",
                resolve: context => "Librame"
            );
        }
    }
}
