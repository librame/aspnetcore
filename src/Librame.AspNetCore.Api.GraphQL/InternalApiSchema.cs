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
    /// 内部 API 架构。
    /// </summary>
    internal class InternalApiSchema : Schema, IApiSchema<IObjectGraphType>
    {
        /// <summary>
        /// 构造一个 <see cref="InternalApiSchema"/> 实例。
        /// </summary>
        /// <param name="query">给定的 <see cref="IApiQuery"/>。</param>
        public InternalApiSchema(IApiQuery query)
        {
            Query = query.NotNull(nameof(query));
        }
    }
}
