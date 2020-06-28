#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Librame.AspNetCore.Web.Builders
{
    using AspNetCore.Web.Applications;
    using AspNetCore.Web.DataAnnotations;
    using AspNetCore.Web.Localizers;
    using AspNetCore.Web.Projects;
    using AspNetCore.Web.Services;
    using AspNetCore.Web.Themepacks;
    using Extensions.Core.Builders;
    using Extensions.Core.Services;

    /// <summary>
    /// Web 构建器。
    /// </summary>
    public class WebBuilder : AbstractExtensionBuilder, IWebBuilder
    {
        /// <summary>
        /// 构造一个 <see cref="WebBuilder"/>。
        /// </summary>
        /// <param name="parentBuilder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependency">给定的 <see cref="WebBuilderDependency"/>。</param>
        public WebBuilder(IExtensionBuilder parentBuilder, WebBuilderDependency dependency)
            : base(parentBuilder, dependency)
        {
            Services.AddSingleton<IWebBuilder>(this);

            AddInternalServices();
        }


        /// <summary>
        /// 支持泛型控制器。
        /// </summary>
        public bool SupportedGenericController { get; private set; }


        private void AddInternalServices()
        {
            // Applications
            AddService<IApplicationContext, ApplicationContext>();

            // DataAnnotations
            Services.TryReplaceAll<IValidationAttributeAdapterProvider, ResetValidationAttributeAdapterProvider>();

            var oldMvcDataAnnotationsMvcOptionsSetupType = typeof(IValidationAttributeAdapterProvider).Assembly
                .GetType("Microsoft.Extensions.DependencyInjection.MvcDataAnnotationsMvcOptionsSetup");

            Services.TryReplaceAll(typeof(IConfigureOptions<MvcOptions>),
                typeof(ResetMvcDataAnnotationsMvcOptionsSetup),
                oldMvcDataAnnotationsMvcOptionsSetupType);

            // Localizers
            AddService(typeof(IDictionaryHtmlLocalizer<>), typeof(DictionaryHtmlLocalizer<>));
            AddService<IDictionaryHtmlLocalizerFactory, DictionaryHtmlLocalizerFactory>();

            // Projects
            AddService<IProjectContext, ProjectContext>();

            // Services
            AddService<ICopyrightService, CopyrightService>();
            AddService<IUserPortraitService, UserPortraitService>();

            // Themepacks
            AddService<IThemepackContext, ThemepackContext>();
        }


        /// <summary>
        /// 获取指定服务类型的特征。
        /// </summary>
        /// <param name="serviceType">给定的服务类型。</param>
        /// <returns>返回 <see cref="ServiceCharacteristics"/>。</returns>
        public override ServiceCharacteristics GetServiceCharacteristics(Type serviceType)
            => WebBuilderServiceCharacteristicsRegistration.Register.GetOrDefault(serviceType);

    }
}
