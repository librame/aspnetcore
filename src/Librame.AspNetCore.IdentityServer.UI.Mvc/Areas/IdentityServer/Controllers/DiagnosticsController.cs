// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Librame.AspNetCore.IdentityServer.UI
{
    using Extensions;

    /// <summary>
    /// Õï¶Ï¿ØÖÆÆ÷¡£
    /// </summary>
    [SecurityHeaders]
    [Authorize]
    public class DiagnosticsController : Controller
    {
        /// <summary>
        /// GET: /Diagnostics
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var localAddresses = new string[] { "127.0.0.1", "::1", HttpContext.Connection.LocalIpAddress.ToString() };
            if (!localAddresses.Contains(HttpContext.Connection.RemoteIpAddress.ToString()))
            {
                return NotFound();
            }

            var model = new DiagnosticsViewModel(await HttpContext.AuthenticateAsync().ConfigureAndResultAsync());
            return View(model);
        }

    }
}