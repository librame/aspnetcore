using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Librame.AspNetCore.Identity.Web.Mvc.Examples
{
    using AspNetCore.Identity.Accessors;
    using AspNetCore.Identity.Builders;
    using AspNetCore.Identity.Stores;
    using AspNetCore.Web.Builders;
    using Extensions;
    using Extensions.Data.Builders;

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
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(options =>
            {
                // SignInManager.SignOutAsync
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                //IdentityConstants.TwoFactorUserIdScheme
            })
            .AddIdentityCookies(cookies => { });

            var mvcBuilder = services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            services.AddLibrameCore()
                .AddDataCore(dependency =>
                {
                    // Use SQL Server
                    dependency.BindDefaultTenant();
                })
                .AddAccessor<IdentityDbContextAccessor>((tenant, optionsBuilder) =>
                {
                    optionsBuilder.UseSqlServer(tenant.DefaultConnectionString,
                        sql => sql.MigrationsAssembly(typeof(IdentityDbContextAccessor).GetAssemblyDisplayName()));
                })
                .AddDatabaseDesignTime<SqlServerDesignTimeServices>()
                .AddStoreIdentifierGenerator<GuidIdentityStoreIdentifierGenerator>()
                .AddStoreInitializer<GuidIdentityStoreInitializer>()
                .AddIdentity<IdentityDbContextAccessor>(dependency =>
                {
                    // Use Librame.AspNetCore.Identity.Web.Mvc RPL
                    dependency.Options.LoginCallbackPath = new PathString("/Identity/Manage/Index");

                    dependency.Identity.Options.Stores.MaxLengthForKeys = 128;
                })
                .AddIdentityWeb()
                .AddIdentityProjectController(mvcBuilder)
                .AddNetwork();
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
