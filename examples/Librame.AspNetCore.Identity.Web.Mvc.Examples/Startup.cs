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
    using Accessors;
    using AspNetCore.Web.Builders;
    using Builders;
    using Extensions;
    using Extensions.Data.Builders;
    using Stores;

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

            // 默认使用测试项目的写入库
            //var defaultConnectionString = "Data Source=.;Initial Catalog=librame_identity_default;Integrated Security=True";
            var writingConnectionString = "Data Source=.;Initial Catalog=librame_identity_writing;Integrated Security=True";

            services.AddLibrameCore()
                .AddDataCore(options =>
                {
                    options.DefaultTenant.DefaultConnectionString = writingConnectionString;
                    options.DefaultTenant.WritingConnectionString = writingConnectionString;
                    options.DefaultTenant.WritingSeparation = false;
                })
                .AddAccessor<IdentityDbContextAccessor>((options, optionsBuilder) =>
                {
                    optionsBuilder.UseSqlServer(options.DefaultTenant.DefaultConnectionString,
                        sql => sql.MigrationsAssembly(typeof(IdentityDbContextAccessor).GetAssemblyDisplayName()));
                })
                .AddDbDesignTime<SqlServerDesignTimeServices>()
                .AddIdentifier<IdentityStoreIdentifier>()
                //.AddInitializer<IdentityStoreInitializer>()
                .AddIdentity<IdentityDbContextAccessor>(dependency =>
                {
                    dependency.Builder.ConfigureOptions = builder =>
                    {
                        // Use Librame.AspNetCore.Identity.Web.Mvc RPL
                        builder.LoginCallbackPath = new PathString("/Idenity/Manage/Index");
                    };

                    dependency.Identity.ConfigureOptions = identity =>
                    {
                        identity.Stores.MaxLengthForKeys = 128;
                    };
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
