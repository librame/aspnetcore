using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Design.Internal;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace Librame.AspNetCore.Identity.Tests
{
    using AspNetCore.Identity.Accessors;
    using AspNetCore.Identity.Stores;
    using Extensions;
    using Extensions.Core.Identifiers;

    internal static class TestServiceProvider
    {
        static TestServiceProvider()
        {
            Current = Current.EnsureSingleton(() =>
            {
                var services = new ServiceCollection();

                services.AddAuthentication(options =>
                {
                    // SignInManager.SignOutAsync
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    // SignInManager.SignInWithClaimsAsync
                    //options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
                });

                services.AddLibrameCore(dependency =>
                {
                    dependency.Options.Identifier.GuidIdentificationGenerator = CombIdentificationGenerator.MySQL;
                })
                .AddData(dependency =>
                {
                    // Use MySQL
                    dependency.Options.DefaultTenant.DefaultConnectionString
                        = MySqlConnectionStringHelper.Validate("server=localhost;port=3306;database=librame_identity_default;user=root;password=123456;");
                    dependency.Options.DefaultTenant.WritingConnectionString
                        = MySqlConnectionStringHelper.Validate("server=localhost;port=3306;database=librame_identity_writing;user=root;password=123456;");

                    dependency.Options.DefaultTenant.WritingSeparation = true;
                    dependency.Options.DefaultTenant.DataSynchronization = true;
                    dependency.Options.DefaultTenant.StructureSynchronization = true;
                })
                .AddAccessorCore<IdentityDbContextAccessor>((tenant, optionsBuilder) =>
                {
                    optionsBuilder.UseMySql(tenant.DefaultConnectionString, mySql =>
                    {
                        mySql.MigrationsAssembly(typeof(IdentityDbContextAccessor).GetAssemblyDisplayName());
                        mySql.ServerVersion(new Version(5, 7, 28), ServerType.MySql);
                    });
                })
                .AddDatabaseDesignTime<MySqlDesignTimeServices>()
                .AddStoreHub<TestStoreHub>()
                .AddStoreIdentifierGenerator<GuidIdentityStoreIdentificationGenerator>()
                .AddStoreInitializer<IdentityStoreInitializer>()
                .AddIdentity<IdentityDbContextAccessor>(dependency =>
                {
                    dependency.Identity.Options.Stores.MaxLengthForKeys = 128;
                });

                return services.BuildServiceProvider();
            });
        }

        public static IServiceProvider Current { get; }
    }
}
