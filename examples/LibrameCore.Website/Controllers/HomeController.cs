#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibrameCore.Website.Controllers
{
    using Entities;

    public class HomeController : Controller
    {
        private readonly IEntityAdapter _adapter;

        public HomeController(IEntityAdapter adapter)
        {
            _adapter = adapter;
        }


        public IActionResult Index()
        {
            var repository = _adapter.GetRepositoryReaderWriter<Article>();
            var model = repository.Get(1);

            _adapter.Logger.LogInformation("Read single article.");

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
