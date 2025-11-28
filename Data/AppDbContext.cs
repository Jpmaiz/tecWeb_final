using Microsoft.EntityFrameworkCore;
using final.Models.Entities;


namespace final.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Celda> Celdas { get; set; } = null!;
        public DbSet<Expediente> Expedientes { get; set; } = null!;
        public DbSet<Guardia> Guardias { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Recluso> Reclusos { get; set; } = null!;

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
