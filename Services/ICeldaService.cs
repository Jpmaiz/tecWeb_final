using final.Models;
using final.Models.DTOs;

namespace final.Services
{
    public interface ICeldaService
    {
        Task<IEnumerable<Celda>> GetAll();
        Task<Celda?> GetOne(Guid id);
        Task<Celda> CreateCelda(CreateCeldaDto dto);
        Task<Celda> UpdateCelda(UpdateCeldaDto dto, Guid id);
        Task DeleteCelda(Guid id);
    }
}
