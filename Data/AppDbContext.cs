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

        public DbSet<Celda> Celdas => Set<Celda>();
        public DbSet<Recluso> Reclusos => Set<Recluso>();
        public DbSet<Expediente> Expedientes => Set<Expediente>();
        public DbSet<Guardia> Guardias => Set<Guardia>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // 1:N Celda -> Reclusos
            // =========================
            modelBuilder.Entity<Recluso>()
                .HasOne(r => r.Celda)
                .WithMany(c => c.Reclusos)
                .HasForeignKey(r => r.CeldaId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // 1:1 Recluso -> Expediente
            // =========================
            modelBuilder.Entity<Expediente>()
                .HasOne(e => e.Recluso)
                .WithOne(r => r.Expediente)
                .HasForeignKey<Expediente>(e => e.ReclusoId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // 1:N Usuario -> Reclusos
            // =========================
            modelBuilder.Entity<Recluso>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.Reclusos)
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // N:M Guardia <-> Recluso (SIN entidad intermedia)
            // =========================
            modelBuilder.Entity<Guardia>()
                .HasMany(g => g.Reclusos)
                .WithMany(r => r.Guardias)
                .UsingEntity(j =>
                    j.ToTable("GuardiaReclusos") // tabla implícita
                );
        }
    }
}
