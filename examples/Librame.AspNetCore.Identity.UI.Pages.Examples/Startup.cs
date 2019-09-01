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

namespace Librame.AspNetCore.Identity.UI.Pages.Examples
{
    using Extensions.Data;
    using Extensions.Network;

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

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies(cookies => { });

            var mvcBuilder = services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            // 默认使用测试项目的写入库
            //var defaultConnectionString = "Data Source=.;Initial Catalog=librame_identity_default;Integrated Security=True";
            var writingConnectionString = "Data Source=.;Initial Catalog=librame_identity_writing;Integrated Security=True";

            services.AddLibrameCore()
                .AddDataCore(options =>
                {
                    options.Tenants.Default.DefaultConnectionString = writingConnectionString;
                    options.Tenants.Default.WritingConnectionString = writingConnectionString;
                    options.Tenants.Default.WritingSeparation = false;
                })
                .AddAccessor<IdentityDbContextAccessor>((options, optionsBuilder) =>
                {
                    var migrationsAssembly = typeof(IdentityDbContextAccessor).Assembly.GetName().Name;
                    optionsBuilder.UseSqlServer(options.Tenants.Default.DefaultConnectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddIdentity<IdentityDbContextAccessor>(dependency =>
                {
                    dependency.BaseSetupAction = options =>
                    {
                        options.Stores.MaxLengthForKeys = 128;
                    };
                })
                .AddIdentityUI()
                .AddIdentityPages(mvcBuilder)
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
