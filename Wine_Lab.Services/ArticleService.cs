using System;
using Wine_Lab.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wine_Lab.Data.Models;
using Wine_Lab.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Wine_Lab.Services
{
    public class ArticleService : IArticle
    {
        private readonly AppDbContext _context;

        public ArticleService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Article> GetAll() => _context.Articles;

        public async Task<Article> GetById(long id) => await _context.Articles.FirstOrDefaultAsync(x => x.Id == id);

        public async Task Add(Article newArticle)
        {
            _context.Articles.Add(newArticle);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Article article)
        {
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var article = await GetById(id);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAll()
        {
            var articles = GetAll();
            foreach (var article in articles)
            {
                _context.Articles.Remove(article);
            }

            await _context.SaveChangesAsync();
        }


    }
}
