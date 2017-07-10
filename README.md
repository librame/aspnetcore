# LibrameCore

Install LibrameCore (C# 7.0)
======================================================
To install LibrameCore (for AspNetCore), run the following command in the Package Manager Console

        PM> Install-Package LibrameCore

Configure Startup.cs
------------------------------------------------------
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // Use SqlServerDbContext (Supported Read Write Separation)
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
        
        ......
        
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
                AutomaticAuthenticate = true,
                LoginPath = "/User/Login"
            });

            // Use LibrameCore
            app.UseLibrameAuthentication<User>();
        }
        
1 Use ArticleController
------------------------------------------------------
        public class ArticleController : Controller
        {
            private readonly IRepository<SqlServerDbContextReader, SqlServerDbContextWriter, Article> _repository;

            public HomeController(IRepository<SqlServerDbContextReader, SqlServerDbContextWriter, Article> repository)
            {
                _repository = repository.NotNull(nameof(repository));
            }

            public IActionResult Index()
            {
                var model = _repository.Get(1);
                
                return View(model);
            }
        }
        
2 Use UserController
------------------------------------------------------
        public class UserController : Controller
        {
            private readonly IUserManager<User> _userManager = null;

            public UserController(IUserManager<User> userManager)
            {
                _userManager = userManager.NotNull(nameof(userManager));
            }
            
            public IActionResult Register()
            {
                return View(new User());
            }
            [AllowAnonymous]
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> RegisterAsync(User user)
            {
                if (!ModelState.IsValid)
                    return View(user);

                // Encode Password
                user.Passwd = _userManager.PasswordManager.Encode(user.Passwd);

                var userResult = await _userManager.CreateAsync(user);

                if (!userResult.IdentityResult.Succeeded)
                {
                    var firstError = userResult.IdentityResult.Errors.FirstOrDefault();

                    if (firstError is LibrameIdentityError)
                        ModelState.AddModelError((firstError as LibrameIdentityError).Key, firstError.Description);
                    else
                        ModelState.AddModelError(string.Empty, firstError.Description);

                    return View(user);
                }

                return RedirectToAction(nameof(UserController.Login));
            }

            [AllowAnonymous]
            [HttpGet]
            public async Task<bool> ValidateRegister(string field, string value)
            {
                return await _userManager.ValidateUniquenessAsync(field, value);
            }
        }
