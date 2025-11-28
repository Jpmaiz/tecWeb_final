using final.Models.Entities;

namespace final.Repositories
{
    public interface IExpedienteRepository
    {
        Task<IEnumerable<Expediente>> GetAll();
        Task<Expediente?> GetOne(Guid id);
        Task Add(Expediente expediente);
        Task Update(Expediente expediente);
        Task Delete(Expediente expediente);
    }
}
