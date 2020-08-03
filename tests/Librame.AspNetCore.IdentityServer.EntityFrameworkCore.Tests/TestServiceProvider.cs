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
    using Models;

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

                services.AddLibrameCore(dependency =>
                {
                    dependency.Options.Identifier.GuidIdentifierGenerator = CombIdentityGenerator.MySQL;
                })
                //.AddEncryption().AddGlobalSigningCredentials() // AddIdentity() Default: AddDeveloperGlobalSigningCredentials()
                .AddData(dependency =>
                {
                    // Use MySQL
                    dependency.Options.DefaultTenant.DefaultConnectionString
                        = MySqlConnectionStringHelper.Validate("server=localhost;port=3306;database=librame_identityserver_default;user=root;password=123456;");
                    dependency.Options.DefaultTenant.WritingConnectionString
                        = MySqlConnectionStringHelper.Validate("server=localhost;port=3306;database=librame_identityserver_writing;user=root;password=123456;");

                    dependency.Options.DefaultTenant.WritingSeparation = true;
                    dependency.Options.DefaultTenant.DataSynchronization = true;
                    dependency.Options.DefaultTenant.StructureSynchronization = true;
                })
                .AddAccessorCore<IdentityServerDbContextAccessor>((tenant, optionsBuilder) =>
                {
                    optionsBuilder.UseMySql(tenant.DefaultConnectionString, mySql =>
                    {
                        mySql.MigrationsAssembly(typeof(IdentityServerDbContextAccessor).GetAssemblyDisplayName());
                        mySql.ServerVersion(new Version(5, 7, 28), ServerType.MySql);
                    });
                })
                .AddDatabaseDesignTime<MySqlDesignTimeServices>()
                .AddStoreIdentifierGenerator<GuidIdentityServerStoreIdentifierGenerator>()
                .AddStoreInitializer<IdentityServerStoreInitializer>()
                .AddStoreHub<TestStoreHub>()
                .AddIdentity<IdentityServerDbContextAccessor>(dependency =>
                {
                    dependency.Identity.Options.Stores.MaxLengthForKeys = 128;
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

                    dependency.Options.Stores.Initialization.DefaultClients
                        .Add(IdentityServerConfiguration.DefaultClient);

                    dependency.Options.Stores.Initialization.DefaultApiResources
                        .Add(IdentityServerConfiguration.DefaultApiResource);

                    dependency.Options.Stores.Initialization.DefaultIdentityResources
                        .AddRange(IdentityServerConfiguration.DefaultIdentityResources);
                })
                .AddAccessorStores<IdentityServerDbContextAccessor>();

                return services.BuildServiceProvider();
            });
        }

        public static IServiceProvider Current { get; }
    }
}
