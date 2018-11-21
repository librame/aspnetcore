using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.Identity.Tests
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
                    .AddIdentity(postConfigureOptions: options =>
                    {
                        options.LocalTenant.DefaultConnectionString = "Data Source=PC-CLOUD\\SQLEXPRESS;Initial Catalog=librame_identity_default;Integrated Security=True";
                        options.LocalTenant.WriteConnectionString = "Data Source=PC-CLOUD\\SQLEXPRESS;Initial Catalog=librame_identity_write;Integrated Security=True";
                        options.LocalTenant.WriteConnectionSeparation = true;
                    })
                    .ConfigureData(builder =>
                    {
                        builder.AddDbContext<IDefaultIdentityDbContext, DefaultIdentityDbContext>((options, optionsBuilder) =>
                        {
                            var migrationsAssembly = typeof(TestServiceProvider).Assembly.GetName().Name;
                            optionsBuilder.UseSqlServer(options.LocalTenant.DefaultConnectionString,
                                sql => sql.MigrationsAssembly(migrationsAssembly));
                        });
                    })
                    .AddCore<IdentityUser, IdentityRole, DefaultIdentityDbContext>(options =>
                    {
                        options.Stores.MaxLengthForKeys = 128;
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
