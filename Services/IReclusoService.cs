using final.Models.DTOs;

namespace final.Services
{
    public interface IReclusoService
    {
        Task<List<ReclusoDto>> GetAll();
        Task<ReclusoDto> Create(CreateReclusoDto dto);
        Task<ReclusoDto> Update(Guid id, UpdateReclusoDto dto);
        Task<bool> Delete(Guid id);
    }
}
