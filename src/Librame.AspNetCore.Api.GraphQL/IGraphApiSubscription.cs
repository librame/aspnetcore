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

namespace Librame.AspNetCore.Api
{
    using Extensions.Core.Services;

    /// <summary>
    /// 图形 API 订阅接口。
    /// </summary>
    public interface IGraphApiSubscription : IObjectGraphType, IService
    {
    }
}
