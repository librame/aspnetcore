using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Librame.AspNetCore.IdentityServer.Api
{
    using AspNetCore.Identity.Accessors;
    using AspNetCore.Identity.Stores;
    using Extensions;

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

            services.AddLibrameCore()
                //.AddEncryption().AddGlobalSigningCredentials() // AddIdentity() Default: AddDeveloperGlobalSigningCredentials()
                .AddData(dependency =>
                {
                    // Use SQL Server
                    dependency.BindDefaultTenant();
                })
                .AddAccessorCore<IdentityDbContextAccessor>((tenant, optionsBuilder) =>
                {
                    optionsBuilder.UseSqlServer(tenant.DefaultConnectionString,
                        sql => sql.MigrationsAssembly(typeof(IdentityDbContextAccessor).GetAssemblyDisplayName()));
                })
                .AddDatabaseDesignTime<SqlServerDesignTimeServices>()
                .AddStoreHub<IdentityStoreHub>()
                .AddStoreIdentifierGenerator<GuidIdentityStoreIdentityGenerator>()
                .AddStoreInitializer<GuidIdentityStoreInitializer>()
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
