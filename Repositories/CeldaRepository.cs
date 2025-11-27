using Microsoft.EntityFrameworkCore;
using final.Data;
using final.Models;

namespace final.Repositories
{
    public class CeldaRepository : ICeldaRepository
    {
        private readonly AppDbContext _db;
        public CeldaRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Add(Celda celda)
        {
            await _db.Celdas.AddAsync(celda);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Celda>> GetAll()
        {
            return await _db.Celdas.ToListAsync();
        }

        public async Task<Celda?> GetOne(Guid id)
        {
            return await _db.Celdas.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Celda celda)
        {
            _db.Celdas.Update(celda);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Celda celda)
        {
            _db.Celdas.Remove(celda);
            await _db.SaveChangesAsync();
        }
    }
}