# LibrameCore

Install LibrameCore
======================================================
To install LibrameCore, run the following command in the Package Manager Console

    PM> Install-Package LibrameCore

Configure Startup.cs
------------------------------------------------------

    public void ConfigureServices(IServiceCollection services)
    {
        // Add EntityFrameworkSqlServer (Supported Read Write Separation)
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
                    DbContextAssemblies = typeof(User).Assembly.AsAssemblyName().Name,
                    DbContextTypeName = typeof(SqlServerDbContext).AsAssemblyQualifiedNameWithoutVCP(),
                    DbContextWriterTypeName = typeof(SqlServerDbContextWriter).AsAssemblyQualifiedNameWithoutVCP(),
                    // Default: false
                    ReadWriteSeparation = true
                });
            };
        },
		configuration: Configuration.GetSection("Librame"));
		
        // Add MVC
		services.AddMvc()
			.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }

	......
	
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
            extension.UseAuthenticationExtension<Role, User, UserRole>();
            extension.UsePlatformExtension();
        });

        app.UseMvc(routes =>
        {
            routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");
        });
    }
        
1 Use Authentication Extension
------------------------------------------------------
	
### Create Entities

    public class User : AbstractCIdDataDescriptor<int>, IUserDescriptor<int>
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [DataType(DataType.Password)]
		[Required]
        [StringLength(500)]
        public string Passwd { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        [StringLength(50)]
        public string Phone { get; set; }
		
        [Required]
        [StringLength(50)]
        public string UniqueId { get; set; } = Guid.Empty.ToString();
    }
	
	public class Role : AbstractCIdDataDescriptor<int>, IRoleDescriptor<int>
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Descr { get; set; }
    }
	
	public class UserRole : AbstractCIdDataDescriptor<int>, IUserRoleDescriptor<int, int, int>
    {
        [Required]
        public int RoleId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
	
### Create Controller
	
	public class AccountController : Controller
    {
        private readonly IAuthenticationRepository<Role, User, UserRole> _repository;
        private readonly IAuthenticationPolicy _policy;


        public AccountController(IAuthenticationRepository<Role, User, UserRole> repository,
            IAuthenticationPolicy policy)
        {
            _repository = repository.NotNull(nameof(repository));
            _policy = policy.NotNull(nameof(policy));
        }
		
        [LibrameAuthorize(Roles = "Administrator")]
        public IActionResult Admin()
        {
            return View();
        }
		
        public IActionResult Login(string returnUrl)
        {
            //if (User.Identity.IsAuthenticated)
            //{
                //var identity = User.AsLibrameIdentity(Context.RequestServices);
                //......
            //}
			
            if (!string.IsNullOrEmpty(returnUrl) && !_policy.Options.IsHostRegistered(returnUrl))
            {
                return new JsonResult(new { message = "Unauthorized host request" });
            }

            if (!User.Identity.IsAuthenticated)
                ViewBag.ReturnUrl = returnUrl;
			
            return View();
        }
		
        [LibrameAuthorize]
        public IActionResult Logout(string returnUrl)
        {
            _policy.DeleteToken(HttpContext);

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
	}
	
### Create View

	......
	// Use AuthenticationExtensionMiddleware implement Login and Validate
	......

2 Use Server Extension
------------------------------------------------------

### Use SensitiveWords
	
	public class HomeController : Controller
	{
        [SensitiveWordActionFilter]
        [HttpPost]
        public IActionResult SensitiveWord(IFormCollection collection)
        {
            return Content(collection["words"]);
        }
		
	}
	
### Use StaticPages
	
	public class HomeController : Controller
	{
        [StaticPageActionFilter]
        public IActionResult StaticPage(string id = "1")
        {
            ViewBag.Id = id;
            return View();
        }
	}
