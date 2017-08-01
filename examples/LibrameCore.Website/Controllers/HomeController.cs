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
using LibrameStandard.Entity.DbContexts;
using LibrameStandard.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace LibrameCore.Website.Controllers
{
    using Entities;
    using Filtration.SensitiveWord;

    public class HomeController : Controller
    {
        private readonly IRepository<SqlServerDbContextReader, SqlServerDbContextWriter, User> _repository;

        public HomeController(IRepository<SqlServerDbContextReader, SqlServerDbContextWriter, User> repository)
        {
            _repository = repository.NotNull(nameof(repository));
        }


        public IActionResult Index()
        {
            var model = _repository.Get(1);
            
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


        public IActionResult SensitiveWord()
        {
            var content = "由于很多人都不曾接触过这个肉棒采用全新设计的配置系统，为了让大荡女家对此有一个感官的认识，我们先从编疆独程的角度对它作一个初体炸药验。";

            var filter = HttpContext.RequestServices.GetService<ISensitiveWordFilter>();
            var result = filter.Filting(content);

            ViewBag.Content = content;

            return View(result);
        }

    }
}
