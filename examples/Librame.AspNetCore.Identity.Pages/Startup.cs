using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Linq;

namespace Librame.AspNetCore.Identity.Pages
{
    using Builders;
    using Extensions;
    using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;
    using Resources;

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
            
            var mvcBuilder = services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //.AddViewLocalization()
            //.AddDataAnnotationsLocalization();
            
            services.TryReplace(typeof(IConfigureOptions<MvcOptions>), typeof(MvcDataAnnotationsMvcOptionsSetup),
                typeof(TestMvcDataAnnotationsMvcOptionsSetup));

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var cultures = new[]
                {
                    new CultureInfo("en-US")
                };

                options.DefaultRequestCulture = new RequestCulture("en-US", "en-US");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

            // AddAuthentication
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityConstants.ApplicationScheme;
                o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies(o => { });

            // AddLibrameCore
            services.AddLibrameCore()
                .AddData<IdentityBuilderOptions>(options =>
                {
                    options.LocalTenant.DefaultConnectionString = "Data Source=PC-CLOUD\\SQLEXPRESS;Initial Catalog=librame_identity_default;Integrated Security=True";
                    options.LocalTenant.WriteConnectionString = "Data Source=PC-CLOUD\\SQLEXPRESS;Initial Catalog=librame_identity_write;Integrated Security=True";
                    options.LocalTenant.WriteConnectionSeparation = false;
                })
                .AddDbContext<IIdentityDbContext, IdentityDbContext, IdentityBuilderOptions>((options, optionsBuilder) =>
                {
                    var migrationsAssembly = typeof(IdentityDbContext).Assembly.GetName().Name;
                    optionsBuilder.UseSqlServer(options.LocalTenant.DefaultConnectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddIdentity<IdentityUser, IdentityRole, IdentityDbContext>(configureCoreOptions: coreOptions =>
                {
                    coreOptions.Stores.MaxLengthForKeys = 128;
                })
                .WithUI(builder =>
                {
                    builder.AddUIPages(mvcBuilder);
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

            app.UseMvcWithDefaultRoute();
        }

    }
}
