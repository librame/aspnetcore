using LibrameCore.Abstractions;
using LibrameStandard.Extensions.Entity.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace LibrameCore.WebPage
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
            // Add EntityFrameworkSqlServer
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<SqlServerDbContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("SqlServerDbContext"), sql =>
                    {
                        sql.UseRowNumberForPaging();
                        sql.MaxBatchSize(50);
                    });
                })
                .AddDbContext<SqlServerDbContextWriter>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("SqlServerDbContextWriter"), sql =>
                    {
                        sql.UseRowNumberForPaging();
                        sql.MaxBatchSize(50);
                    });
                });

            // Add CookiePolicy
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Add LibrameCore
            services.AddLibrameCore(configuration: Configuration.GetSection("Librame"));

            //services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Add NLog Configuration
            NLog.LogManager.LoadConfiguration("../../../configs/nlog.config");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            // Configure NLog
            loggerFactory.AddNLog(new NLogProviderOptions
            {
                CaptureMessageTemplates = true,
                CaptureMessageProperties = true
            });

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

            // Use LibrameCore
            app.UseLibrameCore(extension =>
            {
                extension.UsePlatformExtension();
            });

            //app.UseMvc();
        }
    }
}
