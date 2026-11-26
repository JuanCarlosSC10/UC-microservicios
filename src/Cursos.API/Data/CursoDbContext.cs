using Microsoft.EntityFrameworkCore;
using Cursos.API.Models;


namespace Cursos.API.Data
{
    public class CursoDbContext : DbContext
    {
        public CursoDbContext(DbContextOptions<CursoDbContext> options) : base(options) { }

        public DbSet<Curso> Cursos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curso>(entidad =>
            {
                entidad.HasKey(c => c.Id);
                entidad.Property(c => c.Codigo).IsRequired().HasMaxLength(20);
                entidad.Property(c => c.Nombre).IsRequired().HasMaxLength(150);
                entidad.Property(c => c.Creditos).IsRequired();
                entidad.Property(c => c.Docente).IsRequired().HasMaxLength(120);

                entidad.HasIndex(c => c.Codigo).IsUnique();
            });
        }

    }
}
