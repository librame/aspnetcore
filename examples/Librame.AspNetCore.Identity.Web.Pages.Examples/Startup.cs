using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Librame.AspNetCore.Identity.Web.Pages.Examples
{
    using AspNetCore.Identity.Accessors;
    using AspNetCore.Identity.Builders;
    using AspNetCore.Identity.Stores;
    using AspNetCore.Web.Builders;
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

            var mvcBuilder = services.AddRazorPages(options =>
            {
                // Examples
                options.UsePagesRouteStartWithHomeIndex();
            })
            .AddViewLocalization()
            .AddDataAnnotationsLocalization();

            services.AddLibrameCore()
                //.AddEncryption().AddGlobalSigningCredentials() // AddIdentity() Default: AddDeveloperGlobalSigningCredentials()
                .AddNetwork()
                .AddData(dependency =>
                {
                    // Use SQL Server
                    dependency.BindDefaultTenant();
                })
                .AddAccessorCore<DemoIdentityDbContextAccessor>((tenant, optionsBuilder) =>
                {
                    optionsBuilder.UseSqlServer(tenant.DefaultConnectionString,
                        sql => sql.MigrationsAssembly(typeof(DemoIdentityDbContextAccessor).GetAssemblyDisplayName()));
                })
                .AddDatabaseDesignTime<SqlServerDesignTimeServices>()
                .AddStoreIdentifierGenerator<GuidIdentityStoreIdentifierGenerator>()
                .AddStoreInitializer<GuidIdentityStoreInitializer>()
                .AddDemoIdentity<DemoIdentityDbContextAccessor>(dependency =>
                {
                    dependency.Identity.Options.Stores.MaxLengthForKeys = 128;
                })
                .AddIdentityWeb()
                .AddIdentityProjectPage(mvcBuilder);
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseLibrameCore()
                .UsePagesEndpoints();
        }

    }
}
