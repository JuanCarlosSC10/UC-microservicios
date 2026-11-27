using Microsoft.EntityFrameworkCore;
using Estudiantes.API.Models;

namespace Estudiantes.API.Data
{
    public class EstudianteDbContext : DbContext
    {
        public EstudianteDbContext(DbContextOptions<EstudianteDbContext> options) : base(options)
        { }

        public DbSet<Estudiante> Estudiantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudiante>(entidad =>
            {
                entidad.Property(e => e.Codigo).IsRequired().HasMaxLength(20);
                entidad.Property(e => e.Nombres).IsRequired().HasMaxLength(100);
                entidad.Property(e => e.Apellidos).IsRequired().HasMaxLength(100);
                entidad.Property(e => e.Correo).IsRequired().HasMaxLength(150);
                entidad.Property(e => e.Carrera).IsRequired().HasMaxLength(100);

                entidad.HasIndex(e => e.Codigo).IsUnique();
                entidad.HasIndex(e => e.Correo).IsUnique();
            });
        }

    }
}
