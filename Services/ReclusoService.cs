using final.Models.DTOs;
using final.Models.Entities;
using final.Repositories;

namespace final.Services
{
    public class ReclusoService
    {
        private readonly IReclusoRepository _repo;

        public ReclusoService(IReclusoRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ReclusoDto>> GetAll()
        {
            var list = await _repo.GetAll();

            return list.Select(r => new ReclusoDto
            {
                Id = r.Id,
                Nombre = r.Nombre,
                CI = r.CI,
                Delito = r.Delito,
                Celda = r.Celda
            }).ToList();
        }

        public async Task<ReclusoDto> Create(CreateReclusoDto dto)
        {
            var r = new Recluso
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                CI = dto.CI,
                Delito = dto.Delito,
                Celda = dto.Celda
            };

            await _repo.Add(r);

            return new ReclusoDto
            {
                Id = r.Id,
                Nombre = r.Nombre,
                CI = r.CI,
                Delito = r.Delito,
                Celda = r.Celda
            };
        }

        public async Task<ReclusoDto> Update(Guid id, UpdateReclusoDto dto)
        {
            var r = await _repo.GetById(id);
            if (r == null) throw new Exception("Recluso no encontrado");

            r.Nombre = dto.Nombre;
            r.CI = dto.CI;
            r.Delito = dto.Delito;
            r.Celda = dto.Celda;

            await _repo.Update(r);

            return new ReclusoDto
            {
                Id = r.Id,
                Nombre = r.Nombre,
                CI = r.CI,
                Delito = r.Delito,
                Celda = r.Celda
            };
        }

        public async Task<bool> Delete(Guid id)
        {
            var r = await _repo.GetById(id);
            if (r == null) return false;

            await _repo.Delete(r);
            return true;
        }
    }
}
