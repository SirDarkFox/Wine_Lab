using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wine_Lab.Data.Models;
using Wine_Lab.Services.Interfaces;
using Wine_Lab.ViewModels.Article;

namespace Wine_Lab.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticle _articleService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticleController(IArticle articleService, IWebHostEnvironment hostingEnvironment)
        {
            _articleService = articleService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var articles = _articleService.GetAll();
            var model = articles.Select(article => new ArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                ImgPath = article.ImgPath
            });

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _articleService.GetById((int)id);
            if (article == null)
            {
                return NotFound();
            }

            var model = new ArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                ImgPath = article.ImgPath
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title, Content, Image")] ArticleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var article = new Article
                    {
                        Title = model.Title,
                        Content = model.Content,
                        ImgPath = await UploadImage(model)
                    };
                    await _articleService.Add(article);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                //Обработка ошибки
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _articleService.GetById((int)id);
            if (article == null)
            {
                return NotFound();
            }

            var model = new ArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                ImgPath = article.ImgPath
            };

            return View(model);
        }

        //Остановился тут

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<string> UploadImage(ArticleViewModel model)
        {
            if (model.Image != null)
            {
                var uploadsFolder = _hostingEnvironment.WebRootPath + "\\images\\books";
                var fileUrl = model.Image.FileName;
                var filePath = Path.Combine(uploadsFolder, fileUrl);
                var stream = new FileStream(filePath, FileMode.Create);
                await model.Image.CopyToAsync(stream);
                stream.Close();

                return @"/images/books/" + fileUrl;
            }

            return "/img/articles/noimage.png";
        }
    }
}
