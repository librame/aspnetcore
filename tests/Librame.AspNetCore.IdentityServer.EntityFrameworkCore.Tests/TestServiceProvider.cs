using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.IdentityServer.Tests
{
    using AspNetCore.Identity;
    using Extensions;
    using Extensions.Data;
    using Extensions.Encryption;

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
                        options.DefaultTenant.DefaultConnectionString = "Data Source=.;Initial Catalog=librame_identity_default;Integrated Security=True";
                        options.DefaultTenant.WritingConnectionString = "Data Source=.;Initial Catalog=librame_identity_writing;Integrated Security=True";
                        options.DefaultTenant.WritingSeparation = true;
                    })
                    .AddAccessor<IdentityServerDbContextAccessor>((options, optionsBuilder) =>
                    {
                        var migrationsAssembly = typeof(IdentityServerDbContextAccessor).Assembly.GetName().Name;
                        optionsBuilder.UseSqlServer(options.DefaultTenant.DefaultConnectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                    })
                    .AddStoreHubWithAccessor<TestStoreHub>()
                    .AddInitializerWithAccessor<IdentityServerStoreInitializer>()
                    .AddIdentifier<IdentityServerStoreIdentifier>()
                    .AddDbDesignTime<SqlServerDesignTimeServices>()
                    .AddIdentity<IdentityServerDbContextAccessor>(options =>
                    {
                        options.Stores.MaxLengthForKeys = 128;
                    })
                    .AddEncryption().AddDeveloperGlobalSigningCredentials()
                    .AddIdentityServer<IdentityServerDbContextAccessor, DefaultIdentityUser<string>>(dependency =>
                    {
                        dependency.IdentityServer = options =>
                        {
                            options.Events.RaiseErrorEvents = true;
                            options.Events.RaiseInformationEvents = true;
                            options.Events.RaiseFailureEvents = true;
                            options.Events.RaiseSuccessEvents = true;
                            options.Authentication.CookieAuthenticationScheme = IdentityConstants.ApplicationScheme;
                        };
                        dependency.Builder.Action = builder =>
                        {
                            builder.Authorizations.Clients.AddIdentityServerSPA("Librame.AspNetCore.IdentityServer.Api", _ => { });
                        };
                    });

                return services.BuildServiceProvider();
            });
        }

        public static IServiceProvider Current { get; }
    }
}
