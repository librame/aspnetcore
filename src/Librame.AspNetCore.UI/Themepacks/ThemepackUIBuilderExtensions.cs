#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

//using Microsoft.Extensions.DependencyInjection;
//using System.Linq;

//namespace Librame.AspNetCore.UI
//{
//    using Extensions;
//    using Extensions.Core;

//    /// <summary>
//    /// 主题 UI 构建器静态扩展。
//    /// </summary>
//    public static class ThemepackUIBuilderExtensions
//    {
//        /// <summary>
//        /// 添加主题集合。
//        /// </summary>
//        /// <param name="builder">给定的 <see cref="IUIBuilder"/>。</param>
//        /// <returns>返回 <see cref="IUIBuilder"/>。</returns>
//        public static IUIBuilder AddThemepacks(this IUIBuilder builder)
//        {
//            builder.Services.AddScoped<IThemepackProvider, InternalThemepackProvider>();

//            builder.Services.AddThemepackInfos();

//            return builder;
//        }

//        private static void AddThemepackInfos(this IServiceCollection services)
//        {
//            var interfaceType = typeof(IThemepackInfo);

//            BuilderGlobalization.RegisterTypes(type =>
//            {
//                services.AddSingleton(interfaceType, type);
//            },
//            types => types
//                .Where(type => interfaceType.IsAssignableFrom(type) && type.IsConcreteType()));
//        }

//    }
//}
