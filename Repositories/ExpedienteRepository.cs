using final.Data;
using final.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace final.Repositories
{
    public class ExpedienteRepository : IExpedienteRepository
    {
        private readonly AppDbContext _db;
        public ExpedienteRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Add(Expediente expediente)
        {
            await _db.Expedientes.AddAsync(expediente);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Expediente>> GetAll()
        {
            return await _db.Expedientes.ToListAsync();
        }

        public async Task<Expediente?> GetOne(Guid id)
        {
            return await _db.Expedientes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Expediente expediente)
        {
            _db.Expedientes.Update(expediente);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Expediente expediente)
        {
            _db.Expedientes.Remove(expediente);
            await _db.SaveChangesAsync();
        }
    }
}