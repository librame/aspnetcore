// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Librame.AspNetCore.IdentityServer.UI
{
    using Extensions;
    using Extensions.Core;

    [SecurityHeaders]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [InjectionService]
        private IIdentityServerInteractionService _interaction;

        [InjectionService]
        private IHostingEnvironment _environment;

        [InjectionService]
        private ILogger<HomeController> _logger;


        public HomeController(IInjectionService injection)
        {
            injection.NotNull(nameof(injection)).Inject(this);
        }


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
            var message = await _interaction.GetErrorContextAsync(errorId);
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