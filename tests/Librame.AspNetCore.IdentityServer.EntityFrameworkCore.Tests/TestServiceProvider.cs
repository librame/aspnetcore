using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.IdentityServer.Tests
{
    using Extensions;
    using Extensions.Data;
    using Extensions.Encryption;
    using Identity;

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
                        // Use Identity Database
                        options.DefaultTenant.DefaultConnectionString = "Data Source=.;Initial Catalog=librame_identityserver_default;Integrated Security=True";
                        options.DefaultTenant.WritingConnectionString = "Data Source=.;Initial Catalog=librame_identityserver_writing;Integrated Security=True";
                        options.DefaultTenant.WritingSeparation = true;
                    })
                    .AddAccessor<IdentityServerDbContextAccessor>((options, optionsBuilder) =>
                    {
                        optionsBuilder.UseSqlServer(options.DefaultTenant.DefaultConnectionString,
                            sql => sql.MigrationsAssembly(typeof(IdentityServerDbContextAccessor).GetSimpleAssemblyName()));
                    })
                    .AddDbDesignTime<SqlServerDesignTimeServices>()
                    .AddIdentifier<IdentityServerStoreIdentifier>()
                    .AddInitializer<TestStoreInitializer>()
                    .AddStoreHub<TestStoreHub>()
                    .AddIdentity<IdentityServerDbContextAccessor>(options =>
                    {
                        options.Stores.MaxLengthForKeys = 128;
                    })
                    .AddIdentityServer<DefaultIdentityUser<string>>(dependency =>
                    {
                        dependency.IdentityServer = options =>
                        {
                            options.Events.RaiseErrorEvents = true;
                            options.Events.RaiseInformationEvents = true;
                            options.Events.RaiseFailureEvents = true;
                            options.Events.RaiseSuccessEvents = true;

                            options.Authentication.CookieAuthenticationScheme = IdentityConstants.ApplicationScheme;
                        };
                    })
                    .AddAccessorStores<IdentityServerDbContextAccessor>()
                    .AddEncryption().AddDeveloperGlobalSigningCredentials();

                return services.BuildServiceProvider();
            });
        }

        public static IServiceProvider Current { get; }
    }
}
