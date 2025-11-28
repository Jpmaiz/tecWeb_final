using final.Models.DTOs;
using final.Models.Entities;
using final.Repositories;

namespace final.Services
{
    public class ReclusoService : IReclusoService
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
                FechaIngreso = r.FechaIngreso,
                CondenaAnios = r.CondenaAnios,
                UsuarioId = r.UsuarioId
            }).ToList();
        }

        public async Task<ReclusoDto> Create(CreateReclusoDto dto)
        {
            var recluso = new Recluso
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                CI = dto.CI,
                FechaIngreso = dto.FechaIngreso,
                CondenaAnios = dto.CondenaAnios,
                UsuarioId = dto.UsuarioId
            };

            await _repo.Add(recluso);

            return new ReclusoDto
            {
                Id = recluso.Id,
                Nombre = recluso.Nombre,
                CI = recluso.CI,
                FechaIngreso = recluso.FechaIngreso,
                CondenaAnios = recluso.CondenaAnios,
                UsuarioId = recluso.UsuarioId
            };
        }

        public async Task<ReclusoDto> Update(Guid id, UpdateReclusoDto dto)
        {
            var recluso = await _repo.GetById(id);
            if (recluso is null)
                throw new Exception("Recluso no encontrado");

            recluso.Nombre = dto.Nombre;
            recluso.CI = dto.CI;
            recluso.FechaIngreso = dto.FechaIngreso;
            recluso.CondenaAnios = dto.CondenaAnios;

            await _repo.Update(recluso);

            return new ReclusoDto
            {
                Id = recluso.Id,
                Nombre = recluso.Nombre,
                CI = recluso.CI,
                FechaIngreso = recluso.FechaIngreso,
                CondenaAnios = recluso.CondenaAnios,
                UsuarioId = recluso.UsuarioId
            };
        }

        public async Task<bool> Delete(Guid id)
        {
            var recluso = await _repo.GetById(id);
            if (recluso is null)
                return false;

            await _repo.Delete(recluso);
            return true;
        }
    }
}
