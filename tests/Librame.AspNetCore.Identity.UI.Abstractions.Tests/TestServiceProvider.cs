using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.Identity.UI.Tests
{
    using Builders;
    using Extensions;

    internal static class TestServiceProvider
    {
        static TestServiceProvider()
        {
            if (Current.IsDefault())
            {
                var services = new ServiceCollection();

                // AddLibrameCore
                services.AddLibrameCore();

                Current = services.BuildServiceProvider();
            }
        }

        public static IServiceProvider Current { get; private set; }
    }
}
