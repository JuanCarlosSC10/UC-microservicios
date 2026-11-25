using Microsoft.EntityFrameworkCore;
using Cursos.API.Models;


namespace Cursos.API.Data
{
    public class CursoDbContext : DbContext
    {
        public CursoDbContext(DbContextOptions<CursoDbContext> options) : base(options) { }

        public DbSet<Curso> Cursos { get; set; }

    }
}
