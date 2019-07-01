#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Configuration;
using System;

namespace Librame.Extensions.Data
{
    using Core;

    /// <summary>
    /// ASP.NET Core 数据构建器静态扩展。
    /// </summary>
    public static class CoreDataBuilderExtensions
    {
        /// <summary>
        /// 添加 ASP.NET Core 数据扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="configureOptions">给定的 <see cref="Action{DataBuilderOptions}"/>（可选；高优先级）。</param>
        /// <param name="configuration">给定的 <see cref="IConfiguration"/>（可选；次优先级）。</param>
        /// <param name="configureBinderOptions">给定的配置绑定器选项动作（可选）。</param>
        /// <returns>返回 <see cref="IDataBuilder"/>。</returns>
        public static IDataBuilder AddCoreData(this IBuilder builder,
            Action<DataBuilderOptions> configureOptions = null,
            IConfiguration configuration = null,
            Action<BinderOptions> configureBinderOptions = null)
        {
            var dataBuilder = builder.AddData(configureOptions,
                configuration, configureBinderOptions);

            return dataBuilder
                .AddCoreServices();
        }

    }
}
