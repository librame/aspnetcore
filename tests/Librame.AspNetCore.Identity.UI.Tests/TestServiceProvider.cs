using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.Identity.UI.Tests
{
    using Extensions;

    internal static class TestServiceProvider
    {
        static TestServiceProvider()
        {
            if (Current.IsNull())
            {
                var services = new ServiceCollection();

                services.AddLibrameCore();

                Current = services.BuildServiceProvider();
            }
        }

        public static IServiceProvider Current { get; private set; }
    }
}
