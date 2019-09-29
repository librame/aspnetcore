#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization.Routing;
using System;
using System.Globalization;

namespace Librame.AspNetCore
{
    using Extensions.Core;

    /// <summary>
    /// CoreBuilderDependencyOptions for ASP.NET Core。
    /// </summary>
    public class AspNetCoreBuilderDependencyOptions : CoreBuilderDependencyOptions
    {
        /// <summary>
        /// 默认文化信息数组。
        /// </summary>
        public static readonly CultureInfo[] DefaultCultureInfos = new[]
        {
            new CultureInfo("en-US"),
            new CultureInfo("zh-CN"),
            new CultureInfo("zh-TW")
        };


        /// <summary>
        /// <see cref="RequestLocalizationOptions"/> 配置动作（默认已对美英、简中、繁中提供支持）。
        /// </summary>
        public Action<RequestLocalizationOptions> RequestLocalizationAction { get; set; }
            = options =>
            {
                options.SupportedCultures = DefaultCultureInfos;
                options.SupportedUICultures = DefaultCultureInfos;
                // Add RouteData
                options.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider { Options = options });
            };
    }
}
