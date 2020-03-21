using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Design.Internal;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace Librame.AspNetCore.IdentityServer.Tests
{
    using Extensions;
    using Extensions.Data.Builders;
    using Extensions.Encryption.Builders;
    using AspNetCore.Identity.Stores;
    using AspNetCore.IdentityServer.Accessors;
    using AspNetCore.IdentityServer.Stores;

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
                        // Use SQL Server
                        //options.Options.DefaultTenant.DefaultConnectionString = "Data Source=.;Initial Catalog=librame_identityserver_default;Integrated Security=True";
                        //options.Options.DefaultTenant.WritingConnectionString = "Data Source=.;Initial Catalog=librame_identityserver_writing;Integrated Security=True";

                        // Use MySQL
                        dependency.Options.DefaultTenant.DefaultConnectionString = MySqlConnectionStringHelper.Validate("Server=localhost;Database=librame_identityserver_default;User=root;Password=123456;");
                        dependency.Options.DefaultTenant.WritingConnectionString = MySqlConnectionStringHelper.Validate("Server=localhost;Database=librame_identityserver_writing;User=root;Password=123456;");

                        dependency.Options.DefaultTenant.WritingSeparation = true;
                    })
                    //.AddAccessor<IdentityServerDbContextAccessor>((options, optionsBuilder) =>
                    //{
                    //    optionsBuilder.UseSqlServer(options.DefaultTenant.DefaultConnectionString,
                    //        sql => sql.MigrationsAssembly(typeof(IdentityServerDbContextAccessor).GetAssemblyDisplayName()));
                    //})
                    //.AddDbDesignTime<SqlServerDesignTimeServices>()
                    .AddAccessor<IdentityServerDbContextAccessor>((options, optionsBuilder) =>
                    {
                        optionsBuilder.UseMySql(options.DefaultTenant.DefaultConnectionString, mySql =>
                        {
                            mySql.MigrationsAssembly(typeof(IdentityServerDbContextAccessor).GetAssemblyDisplayName());
                            mySql.ServerVersion(new Version(5, 7, 28), ServerType.MySql);
                        });
                    })
                    .AddDbDesignTime<MySqlDesignTimeServices>()
                    .AddStoreIdentifier<IdentityServerStoreIdentifier>()
                    .AddStoreInitializer<TestStoreInitializer>()
                    .AddStoreHub<TestStoreHub>()
                    .AddIdentity<IdentityServerDbContextAccessor>(options =>
                    {
                        options.Identity.Options.Stores.MaxLengthForKeys = 128;
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
