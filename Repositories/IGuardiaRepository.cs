using final.Models.Entities;

namespace final.Repositories
{
    public interface IGuardiaRepository
    {
        Task<List<Guardia>> GetAllAsync();
        Task<Guardia?> GetByIdAsync(Guid id);
        Task AddAsync(Guardia guardia);
        Task UpdateAsync(Guardia guardia);
        Task DeleteAsync(Guardia guardia);
        Task<int> SaveChangesAsync();
    }
}
