using final.Models;
using final.Models.DTOs;
using final.Repositories;

namespace final.Services
{
    public class CeldaService : ICeldaService
    {
        private readonly ICeldaRepository _repo;
        public CeldaService(ICeldaRepository repo)
        {
            _repo = repo;
        }

        public async Task<Celda> CreateCelda(CreateCeldaDto dto)
        {
            var celda = new Celda
            {
                Id = Guid.NewGuid(),
                Numero = dto.Numero,
                Pabellon = dto.Pabellon,
                Capacidad = dto.Capacidad
            };
            await _repo.Add(celda);
            return celda;
        }

        public async Task<IEnumerable<Celda>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Celda?> GetOne(Guid id)
        {
            return await _repo.GetOne(id);
        }

        public async Task<Celda> UpdateCelda(UpdateCeldaDto dto, Guid id)
        {
            Celda? celda = await GetOne(id);
            if (celda == null) throw new Exception("Celda no existe.");

            celda.Numero = dto.Numero;
            celda.Pabellon = dto.Pabellon;
            celda.Capacidad = dto.Capacidad;

            await _repo.Update(celda);
            return celda;
        }

        public async Task DeleteCelda(Guid id)
        {
            Celda? celda = await GetOne(id);
            if (celda != null)
            {
                await _repo.Delete(celda);
            }
        }
    }
}
