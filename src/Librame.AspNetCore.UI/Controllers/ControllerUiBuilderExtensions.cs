#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    static class ControllerUiBuilderExtensions
    {
        public static IUiBuilder AddControllers(this IUiBuilder builder)
        {
            if (builder.UserType.IsNotNull()
                && builder.Services.TryGet<ApplicationPartManager>(out ServiceDescriptor serviceDescriptor)
                && serviceDescriptor.ImplementationInstance.IsNotNull())
            {
                var manager = serviceDescriptor.ImplementationInstance as ApplicationPartManager;
                if (!manager.FeatureProviders.OfType<UiTemplateWithUserControllerFeatureProvider>().Any())
                {
                    manager.FeatureProviders.Add(new UiTemplateWithUserControllerFeatureProvider(builder.UserType));
                }
            }

            return builder;
        }

    }
}
