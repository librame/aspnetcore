using System;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Routing;
using LibrameSample.Mvc.Models;

namespace LibrameSample.Mvc
{
    public partial class Startup
    {
        public Startup()
        {
            // Setup configuration sources.
            Configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables(); ;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework(Configuration)
                .AddSqlServer()
                .AddDbContext<SampleDbContext>();

            services.AddMvc();

            // Add Librame Framework
            services.AddLibrame(map => map.MapId("51BF1142-6270-4C86-ADBF-7A4E7117F4EC"));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });

            app.UseLibrame();
        }

    }
}
