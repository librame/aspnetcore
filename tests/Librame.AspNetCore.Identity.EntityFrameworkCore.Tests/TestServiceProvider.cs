using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions;
    using Extensions.Data;

    internal static class TestServiceProvider
    {
        static TestServiceProvider()
        {
            Current = Current.EnsureSingleton(() =>
            {
                var services = new ServiceCollection();

                // Add Authentication
                services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies(cookies => { });

                services.AddLibrameCore()
                    .AddDataCore(options =>
                    {
                        options.Tenants.Default.DefaultConnectionString = "Data Source=.;Initial Catalog=librame_identity_default;Integrated Security=True";
                        options.Tenants.Default.WritingConnectionString = "Data Source=.;Initial Catalog=librame_identity_writing;Integrated Security=True";
                        options.Tenants.Default.WritingSeparation = true;
                    })
                    .AddAccessor<IdentityDbContextAccessor>((options, optionsBuilder) =>
                    {
                        var migrationsAssembly = typeof(IdentityDbContextAccessor).Assembly.GetName().Name;
                        optionsBuilder.UseSqlServer(options.Tenants.Default.DefaultConnectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                    })
                    .AddStoreHub<TestStoreHub>() // IStoreHub<IdentityDbContextAccessor>
                    .AddInitializer<TestIdentityStoreInitializer>() // IStoreInitializer<IdentityDbContextAccessor>
                    .AddIdentifier<IdentityStoreIdentifier>() // IStoreIdentifier
                    .AddIdentity<IdentityDbContextAccessor>(dependency =>
                    {
                        dependency.BaseSetupAction = options =>
                        {
                            options.Stores.MaxLengthForKeys = 128;
                        };
                    });

                return services.BuildServiceProvider();
            });
        }

        public static IServiceProvider Current { get; }
    }
}
