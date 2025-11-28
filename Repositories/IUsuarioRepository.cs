using final.Models.Entities;

namespace final.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByCorreo(string correo);
        Task<Usuario?> GetById(Guid id);
        Task Add(Usuario usuario);
        Task<List<Usuario>> GetAll();
    }
}
