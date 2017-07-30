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

namespace LibrameCore.Website.Controllers
{
    using Entities;
    
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
    }
}
