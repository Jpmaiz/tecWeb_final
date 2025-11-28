using final.Models;
using final.Models.DTOs;
using final.Repositories;

namespace final.Services
{
    public class ExpedienteService : IExpedienteService
    {
        private readonly IExpedienteRepository _repo;
        public ExpedienteService(IExpedienteRepository repo)
        {
            _repo = repo;
        }

        public async Task<Expediente> CreateExpediente(CreateExpedienteDto dto)
        {
            var expediente = new Expediente
            {
                Id = Guid.NewGuid(),
                Codigo = dto.Codigo,
                DelitoPrincipal = dto.DelitoPrincipal,
                FechaRegistro = dto.FechaRegistro,
                ReclusoId = dto.ReclusoId
            };
            await _repo.Add(expediente);
            return expediente;
        }

        public async Task<IEnumerable<Expediente>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Expediente?> GetOne(Guid id)
        {
            return await _repo.GetOne(id);
        }

        public async Task<Expediente> UpdateExpediente(UpdateExpedienteDto dto, Guid id)
        {
            Expediente? expediente = await GetOne(id);
            if (expediente == null) throw new Exception("Expediente no existe.");

            expediente.Codigo = dto.Codigo;
            expediente.DelitoPrincipal = dto.DelitoPrincipal;
            expediente.FechaRegistro = dto.FechaRegistro;
            expediente.ReclusoId = dto.ReclusoId;

            await _repo.Update(expediente);
            return expediente;
        }

        public async Task DeleteExpediente(Guid id)
        {
            Expediente? expediente = await GetOne(id);
            if (expediente != null)
            {
                await _repo.Delete(expediente);
            }
        }
    }
}