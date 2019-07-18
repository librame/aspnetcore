#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Librame.Extensions.Data
{
    using Core;

    /// <summary>
    /// ASP.NET Core 数据构建器静态扩展。
    /// </summary>
    public static class AspNetCoreDataBuilderExtensions
    {
        /// <summary>
        /// 添加 ASP.NET Core 数据扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="setupAction">给定的选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IDataBuilder"/>。</returns>
        public static IDataBuilder AddDataCore(this IExtensionBuilder builder,
            Action<DataBuilderOptions> setupAction = null)
        {
            return builder.AddData(setupAction)
                .AddServices();
        }

        /// <summary>
        /// 添加 ASP.NET Core 数据扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="createFactory">给定创建数据构建器的工厂方法。</param>
        /// <param name="setupAction">给定的选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IDataBuilder"/>。</returns>
        public static IDataBuilder AddDataCore(this IExtensionBuilder builder,
            Func<IExtensionBuilder, IDataBuilder> createFactory,
            Action<DataBuilderOptions> setupAction = null)
        {
            return builder.AddData(createFactory, setupAction)
                .AddServices();
        }

    }
}
