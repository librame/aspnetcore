using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.IdentityServer.UI.Tests
{
    using Extensions;

    internal static class TestServiceProvider
    {
        static TestServiceProvider()
        {
            Current = Current.EnsureSingleton(() =>
            {
                var services = new ServiceCollection();

                services.AddLibrameCore();

                return services.BuildServiceProvider();
            });
        }

        public static IServiceProvider Current { get; private set; }
    }
}
