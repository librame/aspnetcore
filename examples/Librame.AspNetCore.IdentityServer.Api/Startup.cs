using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Librame.AspNetCore.IdentityServer.Api
{
    using Extensions.Data;
    using Extensions.Encryption;
    using Extensions.Network;
    using Identity;
    using Identity.UI;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityConstants.ApplicationScheme;
                o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            //.AddIdentityCookies(o => { })
            .AddIdentityServerJwt();

            var mvcBuilder = services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // 默认使用测试项目的写入库
            //var defaultConnectionString = "Data Source=.;Initial Catalog=librame_identity_default;Integrated Security=True";
            var writingConnectionString = "Data Source=.;Initial Catalog=librame_identity_writing;Integrated Security=True";

            services.AddLibrameCore()
                .AddDataCore(options =>
                {
                    options.DefaultTenant.DefaultConnectionString = writingConnectionString;
                    options.DefaultTenant.WritingConnectionString = writingConnectionString;
                    options.DefaultTenant.WritingSeparation = false;
                })
                .AddAccessor<IdentityServerDbContextAccessor>((options, optionsBuilder) =>
                {
                    var migrationsAssembly = typeof(IdentityServerDbContextAccessor).Assembly.GetName().Name;
                    optionsBuilder.UseSqlServer(options.DefaultTenant.DefaultConnectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddIdentifier<IdentityServerStoreIdentifier>()
                .AddIdentity<IdentityServerDbContextAccessor>(options =>
                {
                    options.Stores.MaxLengthForKeys = 128;
                })
                .AddIdentityUI()
                .AddIdentityInterfaceWithPages(mvcBuilder)
                .AddEncryption().AddDeveloperGlobalSigningCredentials()
                .AddNetwork()
                .AddIdentityServer<IdentityServerDbContextAccessor, DefaultIdentityUser<string>>(dependency =>
                {
                    dependency.RawAction = options =>
                    {
                        options.Events.RaiseErrorEvents = true;
                        options.Events.RaiseInformationEvents = true;
                        options.Events.RaiseFailureEvents = true;
                        options.Events.RaiseSuccessEvents = true;
                        options.Authentication.CookieAuthenticationScheme = IdentityConstants.ApplicationScheme;
                    };
                    dependency.OptionsAction = builder =>
                    {
                        builder.Authorizations.Clients.AddIdentityServerSPA("Librame.AspNetCore.IdentityServer.Api", _ => { });
                    };
                });

            //services.AddIdentityCore<TUser>(o =>
            //{
            //    o.Stores.MaxLengthForKeys = 128;
            //    configureOptions?.Invoke(o);
            //})
            //    .AddDefaultUI()
            //    .AddDefaultTokenProviders();

            //services.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            //services.AddAuthentication();

            

            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = "https://localhost:44303";
            //        options.RequireHttpsMetadata = true;
            //        options.ApiName = "IdentityServer.Api";
            //    });

            //services.AddMvc()
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddMvcCore()
            //    .AddAuthorization()
            //    .AddJsonFormatters();

            //services.AddLibrameCore(options => { }, (extensions, options) =>
            //{
            //    extensions.AddCoreExtensionsWithServerAuthentication(options, opts =>
            //    {
            //        opts.Authority = "https://localhost:44303";
            //        opts.RequireHttpsMetadata = true;
            //        opts.ApiName = "IdentityApi";
            //    });
            //});

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
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
                app.UseHsts();
            }

            //app.UseLibrame().ApplyAuthentication();
            //app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseIdentityServer();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }

    }
}
