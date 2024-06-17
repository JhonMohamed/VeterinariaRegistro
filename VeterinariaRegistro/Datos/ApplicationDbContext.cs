
using Microsoft.EntityFrameworkCore;
using VeterinariaRegistro.Models;


namespace VeterinariaRegistro.Datos
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Mascota> Mascota { get; set; }
        public DbSet<TipoMascota> TipoMascota { get; set; }
        public DbSet<RazaMascota> RazaMascota { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar las relaciones entre las entidades
            modelBuilder.Entity<RazaMascota>()
                .HasOne(r => r.TipoMascota)
                .WithMany(t => t.RazasMascotas)
                .HasForeignKey(r => r.IdTipoMascota);

            modelBuilder.Entity<Mascota>()
               .HasOne(m => m.RazaMascota)
               .WithMany(r => r.Mascotas)
               .HasForeignKey(m => m.IdRazaMascota);

            // Datos semilla
            modelBuilder.Entity<TipoMascota>().HasData(
                new TipoMascota { IdTipo = 1, NombreTipo = "Perro" },
                new TipoMascota { IdTipo = 2, NombreTipo = "Gato" },
                new TipoMascota { IdTipo = 3, NombreTipo = "Pájaro" }
            );

            modelBuilder.Entity<RazaMascota>().HasData(
                new RazaMascota { IdRaza = 1, NombreRaza = "Labrador", IdTipoMascota = 1 },
                new RazaMascota { IdRaza = 2, NombreRaza = "Bulldog", IdTipoMascota = 1 },
                new RazaMascota { IdRaza = 3, NombreRaza = "Siamés", IdTipoMascota = 2 },
                new RazaMascota { IdRaza = 4, NombreRaza = "Persa", IdTipoMascota = 2 },
                new RazaMascota { IdRaza = 5, NombreRaza = "Canario", IdTipoMascota = 3 },
                new RazaMascota { IdRaza = 6, NombreRaza = "Periquito", IdTipoMascota = 3 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}