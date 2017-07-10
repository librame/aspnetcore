using System;
using LibrameCore.Entities;
using LibrameStandard.Entity.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace LibrameCore.Website
{

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            ContentRootPath = env.ContentRootPath;
        }

        public IConfigurationRoot Configuration { get; }

        public string ContentRootPath { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // 默认使用 SqlServerDbContext
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<SqlServerDbContextReader>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("SqlServerReader"), sql =>
                    {
                        sql.UseRowNumberForPaging();
                        sql.MaxBatchSize(50);
                    });
                })
                .AddDbContext<SqlServerDbContextWriter>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("SqlServerWriter"), sql =>
                    {
                        sql.UseRowNumberForPaging();
                        sql.MaxBatchSize(50);
                    });
                });

            // Add LibrameCore
            services.AddLibrameCore(Configuration.GetSection("Librame"), authenticationAction: opts =>
            {
                opts.TokenHandler.Expiration = TimeSpan.FromHours(1);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddNLog().ConfigureNLog("../../configs/nlog.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = Authentication.AuthenticationOptions.DEFAULT_SCHEME,
                // 是否自动启用验证，如果不启用，则即便客服端传输了Cookie信息，服务端也不会主动解析。除了明确配置了 [Authorize(ActiveAuthenticationSchemes = "上面的方案名")] 属性的地方，才会解析，此功能一般用在需要在同一应用中启用多种验证方案的时候。比如分Area.
                AutomaticAuthenticate = true,
                LoginPath = "/User/Login"
            });

            // Use LibrameCore
            app.UseLibrameAuthentication<User>();
        }
    }
}
