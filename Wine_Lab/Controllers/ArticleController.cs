using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _articleService.GetById((long)id);
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
                        ImgPath = await UploadImage(model.Image)
                    };
                    await _articleService.Add(article);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                //Обработка ошибки
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _articleService.GetById((long)id);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id, Title, Content, ImgPath, Image")] ArticleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var article = await _articleService.GetById(id);
                    if (article == null)
                    {
                        return NotFound();
                    }

                    if (model.Image != null)
                    {
                        DeleteImage(article.ImgPath);
                        model.ImgPath = await UploadImage(model.Image);
                    }

                    article.Title = model.Title;
                    article.Content = model.Content;
                    article.ImgPath = model.ImgPath;
                    await _articleService.Edit(article);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                //Обработка ошибки
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _articleService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                //Обработка ошибки
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<string> UploadImage(IFormFile image)
        {
            if (image != null)
            {
                var uploadsFolder = _hostingEnvironment.WebRootPath + "\\img\\articles";
                var imageUrl = image.FileName;
                var filePath = Path.Combine(uploadsFolder, imageUrl);
                var stream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(stream);
                stream.Close();

                return @"/img/articles/" + imageUrl;
            }

            return "/img/articles/noimage.png";
        }

        private void DeleteImage(string imgPath)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, imgPath);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
