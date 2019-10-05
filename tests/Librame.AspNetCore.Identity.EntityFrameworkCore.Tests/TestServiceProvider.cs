using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;
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
                        options.DefaultTenant.DefaultConnectionString = "Data Source=.;Initial Catalog=librame_identity_default;Integrated Security=True";
                        options.DefaultTenant.WritingConnectionString = "Data Source=.;Initial Catalog=librame_identity_writing;Integrated Security=True";
                        options.DefaultTenant.WritingSeparation = true;
                    })
                    .AddAccessor<IdentityDbContextAccessor>((options, optionsBuilder) =>
                    {
                        var migrationsAssembly = typeof(IdentityDbContextAccessor).Assembly.GetName().Name;
                        optionsBuilder.UseSqlServer(options.DefaultTenant.DefaultConnectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                    })
                    .AddStoreHubWithAccessor<TestStoreHub>()
                    .AddInitializerWithAccessor<IdentityStoreInitializer>()
                    .AddIdentifier<IdentityStoreIdentifier>()
                    .AddDbDesignTime<SqlServerDesignTimeServices>()
                    .AddIdentity<IdentityDbContextAccessor>(options =>
                    {
                        options.Stores.MaxLengthForKeys = 128;
                    });

                return services.BuildServiceProvider();
            });
        }

        public static IServiceProvider Current { get; }
    }
}
