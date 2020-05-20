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
using System.Collections.Generic;

namespace Librame.AspNetCore.Web.Builders
{
    using AspNetCore.Web.Applications;
    using AspNetCore.Web.Localizers;
    using AspNetCore.Web.Projects;
    using AspNetCore.Web.Services;
    using AspNetCore.Web.Themepacks;
    using Extensions;
    using Extensions.Core.Services;

    /// <summary>
    /// <see cref="IWebBuilder"/> 服务特征注册。
    /// </summary>
    public static class WebBuilderServiceCharacteristicsRegistration
    {
        private static IServiceCharacteristicsRegister _register;

        /// <summary>
        /// 当前注册器。
        /// </summary>
        public static IServiceCharacteristicsRegister Register
        {
            get => _register.EnsureSingleton(() => new ServiceCharacteristicsRegister(InitializeCharacteristics()));
            set => _register = value.NotNull(nameof(value));
        }


        private static IDictionary<Type, ServiceCharacteristics> InitializeCharacteristics()
        {
            return new Dictionary<Type, ServiceCharacteristics>
            {
                // Applications
                { typeof(IApplicationContext), ServiceCharacteristics.Singleton() },
                { typeof(IApplicationPrincipal), ServiceCharacteristics.Singleton() },

                // Localizers
                { typeof(IDictionaryHtmlLocalizer<>), ServiceCharacteristics.Transient() },
                { typeof(IDictionaryHtmlLocalizerFactory), ServiceCharacteristics.Singleton() },

                // Projects
                { typeof(IProjectContext), ServiceCharacteristics.Singleton() },

                // Services
                { typeof(ICopyrightService), ServiceCharacteristics.Singleton() },
                { typeof(IUserPortraitService), ServiceCharacteristics.Singleton() },

                // Themepacks
                { typeof(IThemepackContext), ServiceCharacteristics.Singleton() }
            };
        }

    }
}
