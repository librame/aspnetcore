using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librame.AspNetCore.IdentityServer.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = "https://localhost:44303";
            //        options.RequireHttpsMetadata = true;
            //        options.ApiName = "IdentityServer.Api";
            //    });

            // Add LibrameCore
            //services.AddLibrameCore(options =>
            //{
            //    options.BuildPathConfiguration = () => new CorePathConfiguration();
            //},
            //(extensions, options) =>
            //{
            //    extensions.AddCoreExtensionsWithOidcAuthentication(options, opts =>
            //    {
            //        opts.SignInScheme = IdentityExtensionConfigurator.Defaults.COOKIE_AUTH_SCHEME;

            //        opts.Authority = "https://localhost:44303";
            //        opts.RequireHttpsMetadata = true;

            //        opts.ClientId = "IdentityClient";
            //        opts.ClientSecret = "secret";
            //        opts.ResponseType = "id_token code";
            //        opts.SaveTokens = true;
            //        opts.GetClaimsFromUserInfoEndpoint = true;

            //        //opts.Scope.Clear();
            //        opts.Scope.Add("IdentityApi");
            //        opts.Scope.Add("offline_access");
            //    });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            //app.UseLibrame().ApplyAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvcWithDefaultRoute();
        }
    }
}
