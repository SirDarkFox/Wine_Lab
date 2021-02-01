using System.Collections.Generic;
using System.Threading.Tasks;
using Wine_Lab.Data.Models;

namespace Wine_Lab.Services.Interfaces
{
    public interface IRegulation
    {
        IEnumerable<Regulation> GetAll();
        Task<Regulation> GetById(long id);

        Task Add(Regulation newRegulation);
        Task Edit(Regulation regulation);
        Task Delete(long id);
        Task DeleteAll();
    }
}
