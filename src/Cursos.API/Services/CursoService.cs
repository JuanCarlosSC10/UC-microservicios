using Cursos.API.Dtos;
using Cursos.API.Data;
using Cursos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cursos.API.Services
{
    public class CursoService : ICursoService
    {
        private readonly CursoDbContext _context;

        public CursoService(CursoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Curso>> ListarAsync()
        {
            return await _context.Cursos.ToListAsync();
        }

        public async Task<Curso?> ObtenerAsync(int id)
        {
            return await _context.Cursos.FindAsync(id);
        }

        public async Task<Curso> CrearAsync(CursoDto dto)
        {
            bool duplicado = await _context.Cursos
                .AnyAsync(c => c.Codigo == dto.Codigo);

            if (duplicado)
            {
                throw new InvalidOperationException("Ya existe un curso con ese codigo, ingrese uno nuevo.");
            }

            var curso = new Curso
            {
                Codigo = dto.Codigo,
                Nombre = dto.Nombre,
                Creditos = dto.Creditos,
                Docente = dto.Docente
            };

            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return curso;
        }

        public async Task<Curso?> ActualizarAsync(int id, CursoDto dto)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return null;
            }

            bool duplicado = await _context.Cursos
                .AnyAsync(c => c.Id != id && c.Codigo == dto.Codigo);

            if (duplicado)
            {
                throw new InvalidOperationException("Ya existe otro curso con ese codigo, ingrese uno nuevo.");
            }

            curso.Codigo = dto.Codigo;
            curso.Nombre = dto.Nombre;
            curso.Creditos = dto.Creditos;
            curso.Docente = dto.Docente;

            await _context.SaveChangesAsync();

            return curso;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return false;
            }

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
