using final.Data;
using final.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace final.Repositories
{
    public class GuardiaRepository : IGuardiaRepository
    {
        private readonly AppDbContext _context;

        public GuardiaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Guardia>> GetAllAsync()
        {
            return await _context.Guardias.ToListAsync();
        }

        public async Task<Guardia?> GetByIdAsync(Guid id)
        {
            return await _context.Guardias.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task AddAsync(Guardia guardia)
        {
            await _context.Guardias.AddAsync(guardia);
        }

        public Task UpdateAsync(Guardia guardia)
        {
            _context.Guardias.Update(guardia);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guardia guardia)
        {
            _context.Guardias.Remove(guardia);
            return Task.CompletedTask;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
