#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameCore.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibrameCore.Website.Controllers
{
    using Entities;

    public class HomeController : Controller
    {
        private readonly IRepository<Article> _repository;

        public HomeController(IRepository<Article> repository)
        {
            _repository = repository;
        }


        public IActionResult Index()
        {
            var model = _repository.Get(1);

            _repository.Logger.LogInformation("Read single article.");

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
