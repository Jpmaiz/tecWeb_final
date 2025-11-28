using final.Models.Entities;

namespace final.Repositories
{
    public interface ICeldaRepository
    {
        Task<IEnumerable<Celda>> GetAll();
        Task<Celda?> GetOne(Guid id);
        Task Add(Celda celda);
        Task Update(Celda celda);
        Task Delete(Celda celda);
    }
}
