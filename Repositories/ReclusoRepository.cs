using final.Data;
using final.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace final.Repositories
{
    public class ReclusoRepository : IReclusoRepository
    {
        private readonly AppDbContext _context;

        public ReclusoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Recluso>> GetAll()
        {
            return await _context.Reclusos
                .Include(r => r.Celda)
                .Include(r => r.Usuario)
                .Include(r => r.Expediente)
                .Include(r => r.Guardias)
                .ToListAsync();
        }

        public async Task<Recluso?> GetById(Guid id)
        {
            return await _context.Reclusos
                .Include(r => r.Celda)
                .Include(r => r.Usuario)
                .Include(r => r.Expediente)
                .Include(r => r.Guardias)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task Add(Recluso r)
        {
            await _context.Reclusos.AddAsync(r);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Recluso r)
        {
            _context.Reclusos.Update(r);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Recluso r)
        {
            _context.Reclusos.Remove(r);
            await _context.SaveChangesAsync();
        }
    }
}
