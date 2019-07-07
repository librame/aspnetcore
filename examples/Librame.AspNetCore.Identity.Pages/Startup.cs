using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace Librame.AspNetCore.Identity.Pages
{
    using Extensions.Data;
    using Extensions.Network;
    using UI;

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
            
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var cultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("zh-CN"),
                    new CultureInfo("zh-TW")
                };

                var defaultCulture = cultures[0];
                options.DefaultRequestCulture = new RequestCulture(defaultCulture, defaultCulture);
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

            // Add Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies(cookies => { });

            // Add Mvc
            var mvcBuilder = services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            // Add Librame for ASP.NET Core
            services.AddLibrameCore()
                .AddCoreData(options =>
                {
                    options.DefaultTenant.DefaultConnectionString = "Data Source=.;Initial Catalog=librame_identity_default;Integrated Security=True";
                    options.DefaultTenant.WriteConnectionString = "Data Source=.;Initial Catalog=librame_identity_write;Integrated Security=True";
                    options.DefaultTenant.WriteConnectionSeparation = false;
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

                    options.ConfigureUIMode = builder =>
                    {
                        builder.AddUIPages(mvcBuilder);
                    };
                })
                .AddNetwork();
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseLibrameCore()
                .UseIdentity();

            app.UseMvcWithDefaultRoute();
        }

    }
}
