// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Librame.AspNetCore.IdentityServer.Web.Controllers
{
    using AspNetCore.IdentityServer.Web.Models;
    using Extensions;

    /// <summary>
    /// Õï¶Ï¿ØÖÆÆ÷¡£
    /// </summary>
    [SecurityHeaders]
    [Authorize]
    [Area(IdentityServerRouteBuilderExtensions.AreaName)]
    [Route(IdentityServerRouteBuilderExtensions.Template)]
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

            var result = await HttpContext.AuthenticateAsync().ConfigureAwait();
            return View(new DiagnosticsViewModel(result));
        }

    }
}