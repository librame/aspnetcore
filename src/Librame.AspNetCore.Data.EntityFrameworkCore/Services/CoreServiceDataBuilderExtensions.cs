#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;

namespace Librame.Extensions.Data
{
    /// <summary>
    /// ASP.NET Core 服务数据构建器静态扩展。
    /// </summary>
    public static class CoreServiceDataBuilderExtensions
    {
        /// <summary>
        /// 添加 ASP.NET Core 服务集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IDataBuilder"/>。</param>
        /// <returns>返回 <see cref="IDataBuilder"/>。</returns>
        public static IDataBuilder AddCoreServices(this IDataBuilder builder)
        {
            builder.Services.TryReplace<ITenantService, InternalHttpTenantService>();

            return builder;
        }

    }
}
