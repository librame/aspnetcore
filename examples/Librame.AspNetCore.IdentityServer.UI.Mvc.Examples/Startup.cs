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

namespace Librame.AspNetCore.IdentityServer.UI.Mvc.Examples
{
    using AspNetCore.UI;
    using Extensions;
    using Extensions.Data;
    using Extensions.Encryption;
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

            var mvcBuilder = services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            // 默认使用测试项目的写入库
            //var defaultConnectionString = "Data Source=.;Initial Catalog=librame_identityserver_default;Integrated Security=True";
            var writingConnectionString = "Data Source=.;Initial Catalog=librame_identityserver_writing;Integrated Security=True";

            services.AddLibrameCore()
                .AddDataCore(options =>
                {
                    options.DefaultTenant.DefaultConnectionString = writingConnectionString;
                    options.DefaultTenant.WritingConnectionString = writingConnectionString;
                    options.DefaultTenant.WritingSeparation = false;
                })
                .AddAccessor<IdentityServerDbContextAccessor>((options, optionsBuilder) =>
                {
                    optionsBuilder.UseSqlServer(options.DefaultTenant.DefaultConnectionString,
                        sql => sql.MigrationsAssembly(typeof(IdentityServerDbContextAccessor).GetSimpleAssemblyName()));
                })
                .AddDbDesignTime<SqlServerDesignTimeServices>()
                .AddIdentifier<IdentityServerStoreIdentifier>()
                .AddIdentity<IdentityServerDbContextAccessor>(options =>
                {
                    options.Stores.MaxLengthForKeys = 128;
                })
                .AddIdentityServer<DefaultIdentityUser<string>>(dependency =>
                {
                    // Use InMemoryStores
                    //dependency.Builder.Action = options =>
                    //{
                    //    options.Authorizations.IdentityResources.AddRange(IdentityServerConfiguration.DefaultIdentityResources);
                    //    options.Authorizations.ApiResources.Add(IdentityServerConfiguration.DefaultApiResource);
                    //    options.Authorizations.Clients.Add(IdentityServerConfiguration.DefaultClient);
                    //};

                    dependency.IdentityServer = server =>
                    {
                        // Use Librame.AspNetCore.IdentityServer.UI.Mvc RPL
                        server.UserInteraction.LoginUrl = RouteDescriptor.ByController("Login", "Account", nameof(IdentityServer));
                        server.UserInteraction.LogoutUrl = RouteDescriptor.ByController("Logout", "Account", nameof(IdentityServer));

                        server.Events.RaiseErrorEvents = true;
                        server.Events.RaiseInformationEvents = true;
                        server.Events.RaiseFailureEvents = true;
                        server.Events.RaiseSuccessEvents = true;
                    };
                })
                .AddAccessorStores<IdentityServerDbContextAccessor>() //.AddAccessorStores<IdentityServerDbContextAccessor>() //.AddInMemoryStores()
                .AddIdentityServerUI()
                .AddIdentityServerInterfaceWithViews(mvcBuilder)
                .AddEncryption().AddDeveloperGlobalSigningCredentials()
                .AddNetwork();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();

            app.UseLibrameCore()
                .UseIdentityServerEndpointRoute();
        }

    }
}
