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

namespace Librame.AspNetCore.IdentityServer.Web.Mvc.Examples
{
    using AspNetCore.Web.Builders;
    using AspNetCore.Web.Routings;
    using AspNetCore.Identity.Builders;
    using AspNetCore.Identity.Stores;
    using AspNetCore.IdentityServer.Accessors;
    using AspNetCore.IdentityServer.Builders;
    using AspNetCore.IdentityServer.Stores;
    using Extensions;
    using Extensions.Data.Builders;
    using Extensions.Encryption.Builders;

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

            services.AddLibrameCore()
                .AddDataCore(dependency =>
                {
                    // Use MySQL
                    dependency.BindDefaultTenant(Configuration.GetSection(nameof(dependency.Options.DefaultTenant)),
                        MySqlConnectionStringHelper.Validate);
                })
                .AddAccessor<IdentityServerDbContextAccessor>((options, optionsBuilder) =>
                {
                    optionsBuilder.UseMySql(options.DefaultTenant.DefaultConnectionString, mySql =>
                    {
                        mySql.MigrationsAssembly(typeof(IdentityServerDbContextAccessor).GetAssemblyDisplayName());
                        mySql.ServerVersion(new Version(5, 7, 28), ServerType.MySql);
                    });
                })
                .AddDbDesignTime<MySqlDesignTimeServices>()
                .AddStoreIdentifier<IdentityServerStoreIdentifier>()
                // 启用 Identity 模块注册功能
                .AddStoreIdentifier<IdentityStoreIdentifier>(sp => sp.GetRequiredService<IdentityServerStoreIdentifier>())
                .AddIdentity<IdentityServerDbContextAccessor>(dependency =>
                {
                    dependency.Identity.Options.Stores.MaxLengthForKeys = 128;
                })
                .AddIdentityWeb(dependency =>
                {
                    dependency.Options.Themepack.CommonHeaderNavigationVisibility = false;
                })
                .AddIdentityProjectController(mvcBuilder)
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
                        // Use Librame.AspNetCore.IdentityServer.Web.Mvc RPL
                        server.UserInteraction.LoginUrl = RouteDescriptor.ByController("Login", "Account", nameof(IdentityServer));
                        server.UserInteraction.LogoutUrl = RouteDescriptor.ByController("Logout", "Account", nameof(IdentityServer));

                        server.Events.RaiseErrorEvents = true;
                        server.Events.RaiseInformationEvents = true;
                        server.Events.RaiseFailureEvents = true;
                        server.Events.RaiseSuccessEvents = true;
                    };
                })
                .AddAccessorStores<IdentityServerDbContextAccessor>() //.AddInMemoryStores()
                .AddIdentityServerWeb()
                .AddIdentityServerProjectController(mvcBuilder)
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
                .UseControllerEndpoints(routes =>
                {
                    routes.MapIdentityServerAreaControllerRoute();
                    routes.MapIdentityAreaControllerRoute();
                });
        }

    }
}
