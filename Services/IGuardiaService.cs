using final.Models.DTOs;

namespace final.Services
{
    public interface IGuardiaService
    {
        Task<List<GuardiaDto>> GetAllAsync();
        Task<GuardiaDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(CreateGuardiaDto dto);
        Task<bool> UpdateAsync(Guid id, UpdateGuardiaDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
