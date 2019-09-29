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
using Microsoft.Extensions.Localization;

namespace Librame.AspNetCore
{
    using Extensions;
    using Extensions.Core;

    static class LocalizerCoreBuilderExtensions
    {
        public static ICoreBuilder AddLocalizers(this ICoreBuilder builder)
        {
            //builder.Services.TryReplace<IStringLocalizerFactory, ExpressionStringLocalizerFactoryCore>();

            var optionsAction = (builder.DependencyOptions as AspNetCoreBuilderDependencyOptions).RequestLocalizationAction;
            if (optionsAction.IsNotNull())
                builder.Services.Configure(optionsAction);

            return builder;
        }

    }
}
