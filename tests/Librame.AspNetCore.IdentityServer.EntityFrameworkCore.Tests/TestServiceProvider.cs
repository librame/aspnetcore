using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Design.Internal;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace Librame.AspNetCore.IdentityServer.Tests
{
    using AspNetCore.Identity.Stores;
    using AspNetCore.IdentityServer.Accessors;
    using AspNetCore.IdentityServer.Stores;
    using Extensions;
    using Extensions.Core.Identifiers;

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
                    //.AddEncryption().AddGlobalSigningCredentials() // AddIdentity() Default: AddDeveloperGlobalSigningCredentials()
                    .AddData(dependency =>
                    {
                        dependency.Options.IdentifierGenerator = CombIdentifierGenerator.MySQL;

                        // Use MySQL
                        dependency.Options.DefaultTenant.DefaultConnectionString
                            = MySqlConnectionStringHelper.Validate("server=localhost;port=3306;database=librame_identityserver_default;user=root;password=123456;");
                        dependency.Options.DefaultTenant.WritingConnectionString
                            = MySqlConnectionStringHelper.Validate("server=localhost;port=3306;database=librame_identityserver_writing;user=root;password=123456;");
                        //dependency.Options.DefaultTenant.WritingSeparation = true;
                    })
                    .AddAccessorCore<DemoIdentityServerDbContextAccessor>((tenant, optionsBuilder) =>
                    {
                        optionsBuilder.UseMySql(tenant.DefaultConnectionString, mySql =>
                        {
                            mySql.MigrationsAssembly(typeof(DemoIdentityServerDbContextAccessor).GetAssemblyDisplayName());
                            mySql.ServerVersion(new Version(5, 7, 28), ServerType.MySql);
                        });
                    })
                    .AddDatabaseDesignTime<MySqlDesignTimeServices>()
                    .AddStoreIdentifierGenerator<GuidIdentityServerStoreIdentifierGenerator>()
                    .AddStoreInitializer<TestGuidIdentityServerStoreInitializer>()
                    .AddStoreHub<TestStoreHub>()
                    .AddDemoIdentity<DemoIdentityServerDbContextAccessor>(options =>
                    {
                        options.Identity.Options.Stores.MaxLengthForKeys = 128;
                    })
                    .AddIdentityServer<DefaultIdentityUser<Guid, Guid>>(dependency =>
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
                    .AddAccessorStores<DemoIdentityServerDbContextAccessor>();

                return services.BuildServiceProvider();
            });
        }

        public static IServiceProvider Current { get; }
    }
}
