using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.Web.Tests
{
    using Extensions;
    using Microsoft.Extensions.Localization;

    internal static class TestServiceProvider
    {
        static TestServiceProvider()
        {
            Current = Current.EnsureSingleton(() =>
            {
                var services = new ServiceCollection();

                services.AddLibrameCore();

                services.TryReplaceAll<IStringLocalizerFactory, TestCoreResourceManagerStringLocalizerFactory>();

                return services.BuildServiceProvider();
            });
        }


        public static IServiceProvider Current { get; }
    }
}
