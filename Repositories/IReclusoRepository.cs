using final.Models.Entities;

namespace final.Repositories
{
    public interface IReclusoRepository
    {
        Task<List<Recluso>> GetAll();
        Task<Recluso?> GetById(Guid id);
        Task Add(Recluso r);
        Task Update(Recluso r);
        Task Delete(Recluso r);
    }
}
