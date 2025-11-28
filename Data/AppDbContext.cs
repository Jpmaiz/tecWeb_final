<<<<<<< HEAD
﻿using final.Models;
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
    }
}
=======
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
>>>>>>> e09d72a092387dfc00b6af8faedb5030a6ffdd85
