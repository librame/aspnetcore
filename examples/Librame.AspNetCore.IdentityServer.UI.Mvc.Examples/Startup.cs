using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Librame.AspNetCore.IdentityServer.UI.Mvc.Examples
{
    using Extensions.Data;
    using Extensions.Network;
    using Identity;

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
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
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
                .AddAccessor<IdentityServerDbContextAccessor>((options, optionsBuilder) =>
                {
                    var migrationsAssembly = typeof(IdentityServerDbContextAccessor).Assembly.GetName().Name;
                    optionsBuilder.UseSqlServer(options.DefaultTenant.DefaultConnectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddIdentifier<IdentityStoreIdentifier>()
                .AddDbDesignTime<SqlServerDesignTimeServices>()
                .AddIdentityServer<IdentityServerDbContextAccessor,
                    DefaultIdentityUser<string>>(options =>
                    {
                        options.Events.RaiseErrorEvents = true;
                        options.Events.RaiseInformationEvents = true;
                        options.Events.RaiseFailureEvents = true;
                        options.Events.RaiseSuccessEvents = true;
                    })
                .AddIdentityServerUI()
                .AddIdentityServerInterfaceWithViews(mvcBuilder)
                .AddNetwork();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthorization();
            app.UseLibrameCore()
                .UseIdentityServer();

            app.UseRouting();
            app.UseEndpoints(routes =>
            {
                routes.MapIdentityServerAreaControllerRoute();

                routes.MapDefaultControllerRoute();
            });
        }

    }
}
