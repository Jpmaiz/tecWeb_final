using final.Models;
using Microsoft.EntityFrameworkCore;

namespace final.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Guardia> Guardias { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Recluso> Reclusos { get; set; } = null!;
    }
}