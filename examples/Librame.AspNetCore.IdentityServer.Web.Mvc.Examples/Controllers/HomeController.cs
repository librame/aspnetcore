// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Librame.AspNetCore.IdentityServer.Web.Mvc.Examples.Controllers
{
    using Extensions;
    using Extensions.Core.Services;
    using Models;

    /// <summary>
    /// 主页控制器。
    /// </summary>
    [SecurityHeaders]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [InjectionService]
        private IIdentityServerInteractionService _interaction = null;

        [InjectionService]
        private IWebHostEnvironment _environment = null;

        [InjectionService]
        private ILogger<HomeController> _logger = null;


        /// <summary>
        /// 构造一个 <see cref="HomeController"/>。
        /// </summary>
        /// <param name="injectionService">给定的 <see cref="IInjectionService"/>。</param>
        public HomeController(IInjectionService injectionService)
        {
            injectionService.NotNull(nameof(injectionService)).Inject(this);
        }


        /// <summary>
        /// GET: /
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (_environment.IsDevelopment())
            {
                // only show in development
                return View();
            }

            _logger.LogInformation("Homepage is disabled in production. Returning 404.");
            return NotFound();
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId).ConfigureAwait();
            if (message != null)
            {
                vm.Error = message;

                if (!_environment.IsDevelopment())
                {
                    // only show in development
                    message.ErrorDescription = null;
                }
            }

            return View("Error", vm);
        }

    }
}