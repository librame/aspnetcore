#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
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

    internal class WebBuilder : AbstractExtensionBuilder, IWebBuilder
    {
        public WebBuilder(IExtensionBuilder parentBuilder, WebBuilderDependency dependency)
            : base(parentBuilder, dependency)
        {
            Services.AddSingleton<IWebBuilder>(this);

            AddWebServices();
        }


        public bool SupportedGenericController { get; private set; }

        public Type UserType { get; private set; }


        public override ServiceCharacteristics GetServiceCharacteristics(Type serviceType)
            => WebBuilderServiceCharacteristicsRegistration.Register.GetOrDefault(serviceType);


        private void AddWebServices()
        {
            // Applications
            AddService<IApplicationContext, ApplicationContext>();
            AddService<IApplicationPrincipal, ApplicationPrincipal>();

            // DataAnnotations
            Services.TryReplace<IValidationAttributeAdapterProvider, ResetValidationAttributeAdapterProvider>();

            var mvcDataAnnotationsMvcOptionsSetupType = typeof(IValidationAttributeAdapterProvider).Assembly
                .GetType("Microsoft.Extensions.DependencyInjection.MvcDataAnnotationsMvcOptionsSetup");
            Services.TryReplace(typeof(IConfigureOptions<MvcOptions>), mvcDataAnnotationsMvcOptionsSetupType,
                typeof(ResetMvcDataAnnotationsMvcOptionsSetup));

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

    }
}
