﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions;
    using Extensions.Data;

    internal static class TestServiceProvider
    {
        static TestServiceProvider()
        {
            Current = Current.EnsureSingleton(() =>
            {
                var services = new ServiceCollection();

                services.AddLibrameCore()
                    .AddCoreData(options =>
                    {
                        options.DefaultTenant.DefaultConnectionString = "Data Source=.;Initial Catalog=librame_identity_default;Integrated Security=True";
                        options.DefaultTenant.WriteConnectionString = "Data Source=.;Initial Catalog=librame_identity_write;Integrated Security=True";
                        options.DefaultTenant.WriteConnectionSeparation = true;
                    })
                    .AddAccessor<IdentityDbContextAccessor>((options, optionsBuilder) =>
                    {
                        var migrationsAssembly = typeof(IdentityDbContextAccessor).Assembly.GetName().Name;
                        optionsBuilder.UseSqlServer(options.DefaultTenant.DefaultConnectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                    })
                    .AddIdentity<IdentityDbContextAccessor>(options =>
                    {
                        options.ConfigureCoreIdentity = core =>
                        {
                            core.Stores.MaxLengthForKeys = 128;
                        };
                    });

                services.AddScoped<ITestStore, TestStore>();

                return services.BuildServiceProvider();
            });
        }

        public static IServiceProvider Current { get; }
    }
}
