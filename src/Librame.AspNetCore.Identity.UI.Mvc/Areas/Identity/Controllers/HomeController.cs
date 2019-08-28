#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc;

namespace Librame.AspNetCore.Identity.UI.Controllers
{
    /// <summary>
    /// Ö÷Ò³¿ØÖÆÆ÷¡£
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Get: /
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

    }
}