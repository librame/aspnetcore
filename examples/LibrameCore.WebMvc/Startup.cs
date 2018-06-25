using LibrameStandard;
using LibrameStandard.Extensions.Entity.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace LibrameCore.WebMvc
{
    using Entities;

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
            
            // Add LibrameCore
            services.AddLibrameCore(options =>
            {
                options.PostConfigureAuthentication = opts =>
                {
                    opts.Cookie.Expiration = TimeSpan.FromMinutes(5);
                };

                options.PostConfigureEntity = opts =>
                {
                    opts.Automappings.Add(new EntityExtensionOptions.AutomappingOptions
                    {
                        // 修改默认的自映射实体程序集
                        DbContextAssemblies = typeof(User).Assembly.AsAssemblyName().Name,
                        // 修改默认的数据库上下文类型名为 SQLServer
                        DbContextTypeName = typeof(SqlServerDbContext).AsAssemblyQualifiedNameWithoutVCP(),
                        DbContextWriterTypeName = typeof(SqlServerDbContextWriter).AsAssemblyQualifiedNameWithoutVCP(),
                        // 启用读写分离（默认不启用）
                        ReadWriteSeparation = false
                    });
                };
            },
            Configuration.GetSection("Librame"));

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                extension.UseAuthenticationExtension<Role, User, UserRole, int, int, int>();
                extension.UsePlatformExtension();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
