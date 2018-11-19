using LibrameStandard.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace LibrameCore.IdentityServer
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            services.AddLibrameCore(options =>
            {
                options.BuildPathConfiguration = () => new CorePathConfiguration();
            },
            (extensions, options) =>
            {
                var tuple = extensions.AddCoreExtensionsWithEfCoreForIdentity(options);
                tuple.EntityConfigurator.ApplyMsSqlServer();
                tuple.IdentityConfigurator.ApplyServerWithEfCore(opts =>
                {
                    opts.Events.RaiseErrorEvents = true;
                    opts.Events.RaiseInformationEvents = true;
                    opts.Events.RaiseFailureEvents = true;
                    opts.Events.RaiseSuccessEvents = true;
                })
                .ApplyMsSqlServerStores();
            });
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseLibrame().ApplyIdentityServer(); //app.UseIdentityServer();

            app.UseMvcWithDefaultRoute();
        }

    }
}
