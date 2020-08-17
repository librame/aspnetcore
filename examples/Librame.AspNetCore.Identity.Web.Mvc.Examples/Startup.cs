using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Design.Internal;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace Librame.AspNetCore.Identity.Web.Mvc.Examples
{
    using AspNetCore.Identity.Accessors;
    using AspNetCore.Identity.Builders;
    using AspNetCore.Identity.Stores;
    using AspNetCore.Web.Builders;
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => context.Request.Path.Equals("/");
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            services.AddAuthentication(options =>
            {
                // SignInManager.SignOutAsync
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                // SignInManager.SignInWithClaimsAsync
                //options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            })
            .AddIdentityCookies();

            var mvcBuilder = services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            services.AddLibrameCore(dependency =>
            {
                dependency.Options.Identifier.GuidIdentificationGenerator = CombIdentificationGenerator.MySQL;
            })
            //.AddEncryption().AddGlobalSigningCredentials() // AddIdentity() Default: AddDeveloperGlobalSigningCredentials()
            .AddNetwork()
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
            .AddStoreIdentifierGenerator<GuidIdentityStoreIdentificationGenerator>()
            .AddStoreInitializer<IdentityStoreInitializer>()
            .AddIdentity<IdentityDbContextAccessor>(dependency =>
            {
                dependency.Identity.Options.Stores.MaxLengthForKeys = 128;
            })
            .AddIdentityWeb()
            .AddIdentityProjectController(mvcBuilder);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseLibrameCore()
                .UseControllerEndpoints(routes => routes.MapIdentityAreaControllerRoute());
        }

    }
}
