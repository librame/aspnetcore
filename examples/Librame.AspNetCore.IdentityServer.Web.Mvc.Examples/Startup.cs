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
    using AspNetCore.Identity.Builders;
    using AspNetCore.Identity.Stores;
    using AspNetCore.IdentityServer.Accessors;
    using AspNetCore.IdentityServer.Builders;
    using AspNetCore.IdentityServer.Stores;
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

            var mvcBuilder = services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            services.AddLibrameCore()
                //.AddEncryption().AddGlobalSigningCredentials() // AddIdentity() Default: AddDeveloperGlobalSigningCredentials()
                .AddNetwork()
                .AddData(dependency =>
                {
                    dependency.Options.IdentifierGenerator = CombIdentifierGenerator.MySQL;

                    // Use MySQL
                    dependency.BindDefaultTenant(MySqlConnectionStringHelper.Validate);
                })
                .AddAccessorCore<IdentityServerDbContextAccessor>((tenant, optionsBuilder) =>
                {
                    optionsBuilder.UseMySql(tenant.DefaultConnectionString, mySql =>
                    {
                        mySql.MigrationsAssembly(typeof(IdentityServerDbContextAccessor).GetAssemblyDisplayName());
                        mySql.ServerVersion(new Version(5, 7, 28), ServerType.MySql);
                    });
                })
                .AddDatabaseDesignTime<MySqlDesignTimeServices>()
                .AddStoreIdentifierGenerator<GuidIdentityServerStoreIdentificationGenerator>()
                .AddStoreInitializer<GuidIdentityStoreInitializer>()
                .AddIdentity<IdentityServerDbContextAccessor>()
                .AddIdentityWeb(dependency =>
                {
                    dependency.Options.Themepack.CommonHeaderNavigationVisibility = false;
                })
                .AddIdentityProjectController(mvcBuilder)
                .AddIdentityServer(dependency =>
                {
                    // Use InMemoryStores
                    //dependency.Options.Authorizations.IdentityResources.AddRange(IdentityServerConfiguration.DefaultIdentityResources);
                    //dependency.Options.Authorizations.ApiResources.Add(IdentityServerConfiguration.DefaultApiResource);
                    //dependency.Options.Authorizations.Clients.Add(IdentityServerConfiguration.DefaultClient);

                    dependency.IdentityServer = server =>
                    {
                        // Use Librame.AspNetCore.IdentityServer.Web.Mvc RPL
                        server.UserInteraction.LoginUrl = "/IdentityServer/Account/Login";
                        server.UserInteraction.LogoutUrl = "/IdentityServer/Account/Logout";

                        server.Events.RaiseErrorEvents = true;
                        server.Events.RaiseInformationEvents = true;
                        server.Events.RaiseFailureEvents = true;
                        server.Events.RaiseSuccessEvents = true;
                    };
                })
                .AddAccessorStores<IdentityServerDbContextAccessor>() //.AddInMemoryStores()
                .AddIdentityServerWeb()
                .AddIdentityServerProjectController(mvcBuilder);

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
