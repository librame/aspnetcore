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
using System.Globalization;

namespace Librame.AspNetCore.Builders
{
    using Extensions.Core.Builders;
    using Extensions.Core.Dependencies;

    /// <summary>
    /// <see cref="CoreBuilderDependency"/> for ASP.NET Core。
    /// </summary>
    public class AspNetCoreCoreBuilderDependency : CoreBuilderDependency
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
        /// 请求本地化选项依赖。
        /// </summary>
        public OptionsDependency<RequestLocalizationOptions> RequestLocalization { get; set; }
            = new OptionsDependency<RequestLocalizationOptions>(options =>
            {
                options.SupportedCultures = DefaultCultureInfos;
                options.SupportedUICultures = DefaultCultureInfos;
                // Add RouteData
                options.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider { Options = options });
            });
    }
}
