using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.Identity.UI.Tests
{
    using Builders;
    using Extensions;

    internal static class TestServiceProvider
    {
        static TestServiceProvider()
        {
            if (Current.IsDefault())
            {
                var services = new ServiceCollection();

                // AddLibrameCore
                services.AddLibrameCore()
                    .AddData<IdentityBuilderOptions>(options =>
                    {
                        options.LocalTenant.DefaultConnectionString = "Data Source=PC-CLOUD\\SQLEXPRESS;Initial Catalog=librame_identity_default;Integrated Security=True";
                        options.LocalTenant.WriteConnectionString = "Data Source=PC-CLOUD\\SQLEXPRESS;Initial Catalog=librame_identity_write;Integrated Security=True";
                        options.LocalTenant.WriteConnectionSeparation = false;
                    })
                    .AddDbContext<IIdentityDbContext, IdentityDbContext, IdentityBuilderOptions>((options, optionsBuilder) =>
                    {
                        var migrationsAssembly = typeof(IdentityDbContext).Assembly.GetName().Name;
                        optionsBuilder.UseSqlServer(options.LocalTenant.DefaultConnectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                    })
                    .AddIdentity<IdentityUser, IdentityRole, IdentityDbContext>(configureCoreOptions: coreOptions =>
                    {
                        coreOptions.Stores.MaxLengthForKeys = 128;
                    })
                    .ConfigureCore(builder =>
                    {
                        builder.AddSignInManager()
                            .AddDefaultTokenProviders();
                    });

                services.AddTransient<ITestStore, TestStore>();

                Current = services.BuildServiceProvider();
            }
        }

        public static IServiceProvider Current { get; private set; }
    }
}
