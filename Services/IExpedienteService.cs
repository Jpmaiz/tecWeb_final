using final.Models.Entities;
using final.Models.DTOs;

namespace final.Services
{
    public interface IExpedienteService
    {
        Task<IEnumerable<Expediente>> GetAll();
        Task<Expediente?> GetOne(Guid id);
        Task<Expediente> CreateExpediente(CreateExpedienteDto dto);
        Task<Expediente> UpdateExpediente(UpdateExpedienteDto dto, Guid id);
        Task DeleteExpediente(Guid id);
    }
}
