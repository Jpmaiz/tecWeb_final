using final.Models;
using final.Models.DTOs;
using final.Repositories;

namespace final.Services
{
    public class GuardiaService : IGuardiaService
    {
        private readonly IGuardiaRepository _repository;

        public GuardiaService(IGuardiaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GuardiaDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();

            return list.Select(g => new GuardiaDto
            {
                Id = g.Id,
                Nombre = g.Nombre,
                CI = g.CI,
                Turno = g.Turno,
                Rango = g.Rango
            }).ToList();
        }

        public async Task<GuardiaDto?> GetByIdAsync(Guid id)
        {
            var g = await _repository.GetByIdAsync(id);
            if (g == null) return null;

            return new GuardiaDto
            {
                Id = g.Id,
                Nombre = g.Nombre,
                CI = g.CI,
                Turno = g.Turno,
                Rango = g.Rango
            };
        }

        public async Task<Guid> CreateAsync(CreateGuardiaDto dto)
        {
            var entity = new Guardia
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                CI = dto.CI,
                Turno = dto.Turno,
                Rango = dto.Rango
            };

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateGuardiaDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            entity.Nombre = dto.Nombre;
            entity.CI = dto.CI;
            entity.Turno = dto.Turno;
            entity.Rango = dto.Rango;

            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            await _repository.DeleteAsync(entity);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
