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
    using Extensions.Data.Builders;
    using Extensions.Encryption.Builders;

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
                    .AddDataCore(dependency =>
                    {
                        dependency.Options.IdentifierGenerator = CombIdentifierGenerator.MySQL;

                        // Use MySQL
                        dependency.Options.DefaultTenant.EncryptedConnectionStrings = true;
                        dependency.Options.DefaultTenant.DefaultConnectionString
                            = MySqlConnectionStringHelper.Validate("1MtE5ZRWRqRYtm0vkqyTB4xi8E5iB5OnxJFpTfyJpV1YB9r1kQz5QO2wE8nfXDoK6vY/A5fXpJ7M5FtggIfUia31KymmnapwOusOcWiSu5yiZZfUF2iwyTxkGIkWlmY3");
                        dependency.Options.DefaultTenant.WritingConnectionString
                            = MySqlConnectionStringHelper.Validate("1MtE5ZRWRqRYtm0vkqyTB4xi8E5iB5OnxJFpTfyJpV1YB9r1kQz5QO2wE8nfXDoKZDv7B8bfMirykRdf+FGsdq31KymmnapwOusOcWiSu5yiZZfUF2iwyTxkGIkWlmY3");
                        dependency.Options.DefaultTenant.WritingSeparation = true;
                    })
                    .AddAccessor<IdentityServerDbContextAccessor>((tenant, optionsBuilder) =>
                    {
                        optionsBuilder.UseMySql(tenant.DefaultConnectionString, mySql =>
                        {
                            mySql.MigrationsAssembly(typeof(IdentityServerDbContextAccessor).GetAssemblyDisplayName());
                            mySql.ServerVersion(new Version(5, 7, 28), ServerType.MySql);
                        });
                    })
                    .AddDatabaseDesignTime<MySqlDesignTimeServices>()
                    .AddStoreIdentifierGenerator<GuidIdentityServerStoreIdentifierGenerator>()
                    .AddStoreInitializer<TestGuidIdentityServerStoreInitializer>()
                    .AddStoreHub<TestStoreHub>()
                    .AddIdentity<IdentityServerDbContextAccessor>(options =>
                    {
                        options.Identity.Options.Stores.MaxLengthForKeys = 128;
                    })
                    .AddIdentityServer<DefaultIdentityUser<Guid>>(dependency =>
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
