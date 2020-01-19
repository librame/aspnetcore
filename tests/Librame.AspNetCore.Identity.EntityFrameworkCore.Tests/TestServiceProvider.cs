using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.Identity.Tests
{
    using Accessors;
    using Extensions;
    using Extensions.Data.Builders;
    using Stores;

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
                        optionsBuilder.UseSqlServer(options.DefaultTenant.DefaultConnectionString,
                            sql => sql.MigrationsAssembly(typeof(IdentityDbContextAccessor).GetAssemblyDisplayName()));
                    })
                    .AddDbDesignTime<SqlServerDesignTimeServices>()
                    .AddIdentifier<IdentityStoreIdentifier>()
                    .AddInitializer<IdentityStoreInitializer>()
                    .AddStoreHub<TestStoreHub>()
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
