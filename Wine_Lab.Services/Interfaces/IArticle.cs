using System.Collections.Generic;
using System.Threading.Tasks;
using Wine_Lab.Data.Models;

namespace Wine_Lab.Services.Interfaces
{
    public interface IArticle
    {
        IEnumerable<Article> GetAll();
        Task<Article> GetById(long id);

        Task Add(Article newArticle);
        Task Edit(Article article);
        Task Delete(long id);
        Task DeleteAll();
    }
}
