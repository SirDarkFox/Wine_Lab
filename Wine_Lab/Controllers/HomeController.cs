using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Wine_Lab.Models;
using Wine_Lab.Services;
using Wine_Lab.Services.Interfaces;

namespace Wine_Lab.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticle _articleService;

        public HomeController(IArticle articleService)
        {
            _articleService = articleService;
        }

        public IActionResult Index()
        {
            var model = _articleService.GetAll().TakeLast(3).ToList();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
