using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Design.Internal;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace Librame.AspNetCore.IdentityServer.Api
{
    using AspNetCore.Identity.Accessors;
    using AspNetCore.Identity.Stores;
    using Extensions;
    using Extensions.Core.Identifiers;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            // Api Controllers
            services.AddControllers();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:44303";
                    options.RequireHttpsMetadata = true;
                    options.Audience = "Api";
                })
                .AddIdentityCookies();

            services.AddLibrameCore(dependency =>
            {
                dependency.Options.Identifier.GuidIdentificationGenerator = CombIdentificationGenerator.MySQL;
            })
            //.AddEncryption().AddGlobalSigningCredentials() // AddIdentity() Default: AddDeveloperGlobalSigningCredentials()
            .AddData(dependency =>
            {
                // Use MySQL
                dependency.BindDefaultTenant(MySqlConnectionStringHelper.Validate);
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
            .AddStoreHub<IdentityStoreHub>()
            .AddStoreIdentifierGenerator<GuidIdentityStoreIdentificationGenerator>()
            .AddStoreInitializer<IdentityStoreInitializer>()
            .AddIdentity<IdentityDbContextAccessor>(dependency =>
            {
                dependency.Identity.Options.Stores.MaxLengthForKeys = 128;
            })
            .AddIdentityApi();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(routes =>
            {
                routes.MapControllers();
            });

            // /api/graphql
            app.UseLibrameCore()
                .UseApi();
        }

    }
}
