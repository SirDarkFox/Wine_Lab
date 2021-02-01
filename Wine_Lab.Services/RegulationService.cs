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
    public class RegulationService : IRegulation
    {
        private readonly AppDbContext _context;

        public RegulationService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Regulation> GetAll() => _context.Regulations;

        public async Task<Regulation> GetById(long id) => await _context.Regulations.FirstOrDefaultAsync(x => x.Id == id);

        public async Task Add(Regulation newRegulation)
        {
            _context.Regulations.Add(newRegulation);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Regulation regulation)
        {
            _context.Regulations.Update(regulation);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var regulation = await GetById(id);
            _context.Regulations.Remove(regulation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAll()
        {
            var regulations = GetAll();
            foreach (var regulation in regulations)
            {
                _context.Regulations.Remove(regulation);
            }

            await _context.SaveChangesAsync();
        }


    }
}
