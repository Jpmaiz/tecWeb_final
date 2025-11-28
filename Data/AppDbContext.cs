using final.Models;
using Microsoft.EntityFrameworkCore;

namespace final.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Celda> Celdas => Set<Celda>();
        public DbSet<Expediente> Expedientes => Set<Expediente>();
        public DbSet<Guardia> Guardias => Set<Guardia>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Recluso> Reclusos => Set<Recluso>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Celda>();
            modelBuilder.Entity<Expediente>();
            modelBuilder.Entity<Guardia>();
            modelBuilder.Entity<Usuario>();
            modelBuilder.Entity<Recluso>();
        }
    }
}