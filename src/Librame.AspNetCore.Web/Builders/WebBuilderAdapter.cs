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
using System;

namespace Librame.AspNetCore.Web.Builders
{
    using Extensions.Core.Builders;
    using Extensions.Core.Services;

    internal class WebBuilderAdapter : AbstractExtensionBuilderAdapter<IWebBuilder>, IWebBuilder
    {
        public WebBuilderAdapter(IExtensionBuilder parentBuilder, IWebBuilder webBuilder)
            : base(parentBuilder, webBuilder)
        {
            SupportedGenericController = webBuilder.SupportedGenericController;
            UserType = webBuilder.UserType;

            Services.AddSingleton<IExtensionBuilderAdapter<IWebBuilder>>(this);
        }


        public bool SupportedGenericController { get; private set; }

        public Type UserType { get; private set; }


        public override ServiceCharacteristics GetServiceCharacteristics(Type serviceType)
            => WebBuilderServiceCharacteristicsRegistration.Register.GetOrDefault(serviceType);

    }
}
