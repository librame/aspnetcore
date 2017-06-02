using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace LibrameStandard.Website
{
    using Entity.Providers;
    
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

            //// Librame 实体模块默认使用 DbContextProvider，也可更换为自己的需要，但需同时修改相关配置（详情参见 tests 实体测试）
            //services.AddEntityFrameworkSqlServer().AddDbContext<DbContextProvider>(options =>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("SqlServer"), sql =>
            //    {
            //        sql.UseRowNumberForPaging();
            //        sql.MaxBatchSize(50);
            //    });
            //});

            // Librame MVC
            services.AddLibrameMvc(Configuration.GetSection("Librame"))
                .GetEntityAdapter(Configuration.GetConnectionString("SqlServer")); // 因之前未注册 AddEntityFrameworkSqlServer，此处使用内部集成注册，因此连接字符串不能为空
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

            // Librame MVC
            app.UseLibrameMvc();
        }
    }
}
