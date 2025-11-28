using final.Data;
using final.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace final.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByCorreo(string correo)
        {
            return await _context.Usuarios
                                 .FirstOrDefaultAsync(u => u.Correo == correo);
        }

        public async Task<Usuario?> GetById(Guid id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task Add(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Usuario>> GetAll()
        {
            return await _context.Usuarios.ToListAsync();
        }
    }
}
