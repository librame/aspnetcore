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
    /// 内部 Graph API 变化。
    /// </summary>
    internal class InternalGraphApiMutation : ObjectGraphType, IGraphApiMutation
    {
        /// <summary>
        /// 构造一个 <see cref="InternalGraphApiMutation"/> 实例。
        /// </summary>
        public InternalGraphApiMutation()
        {
            Field<StringGraphType>
            (
                name: "hello",
                resolve: context => "Librame"
            );
        }
    }
}
