using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions.Data;
    using Extensions;

    internal static class TestServiceProvider
    {
        static TestServiceProvider()
        {
            if (Current.IsNull())
            {
                var services = new ServiceCollection();

                // AddLibrameCore
                services.AddAspNetLibrame()
                    .AddAspNetData(options =>
                    {
                        options.LocalTenant.DefaultConnectionString = "Data Source=PC-CLOUD\\SQLEXPRESS;Initial Catalog=librame_identity_default;Integrated Security=True";
                        options.LocalTenant.WriteConnectionString = "Data Source=PC-CLOUD\\SQLEXPRESS;Initial Catalog=librame_identity_write;Integrated Security=True";
                        options.LocalTenant.WriteConnectionSeparation = true;
                    })
                    .AddAccessor<IdentityDbContextAccessor>((options, optionsBuilder) =>
                    {
                        var migrationsAssembly = typeof(IdentityDbContextAccessor).Assembly.GetName().Name;
                        optionsBuilder.UseSqlServer(options.LocalTenant.DefaultConnectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                    })
                    .AddIdentity<IdentityDbContextAccessor>(options =>
                    {
                        options.ConfigureCoreIdentity = core =>
                        {
                            core.Stores.MaxLengthForKeys = 128;
                        };
                    })
                    .AddDefaults();

                services.AddTransient<ITestStore, TestStore>();

                Current = services.BuildServiceProvider();
            }
        }

        public static IServiceProvider Current { get; private set; }
    }
}
