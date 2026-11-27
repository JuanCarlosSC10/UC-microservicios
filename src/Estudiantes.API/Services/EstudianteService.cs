using Estudiantes.API.Dtos;
using Estudiantes.API.Data;
using Estudiantes.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Estudiantes.API.Services
{
    public class EstudianteService : IEstudianteService
    {

        private readonly EstudianteDbContext _context;

        public EstudianteService(EstudianteDbContext context) 
        {
            _context = context;
        }

        public async Task<List<Estudiante>> ListarAsync()
        {
            return await _context.Estudiantes.ToListAsync();
        }

        public async Task<Estudiante?> ObtenerAsync(int id)
        {
            return await _context.Estudiantes.FindAsync(id);
        }

        public async Task<Estudiante> CrearAsync(EstudianteDto dto)
        {
            bool duplicado = await _context.Estudiantes
                .AnyAsync(e => e.Codigo == dto.Codigo || e.Correo == dto.Correo);

            if (duplicado)
            {
                throw new InvalidOperationException("Ya existe un estudiante con ese codigo o correo, ingrese uno nuevo.");
            }

            var estudiante = new Estudiante
            {
                Codigo = dto.Codigo,
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                Correo = dto.Correo,
                Carrera = dto.Carrera
            };

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            return estudiante;
        }

        public async Task<Estudiante?> ActualizarAsync(int id, EstudianteDto dto)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if(estudiante == null)
            {
                return null;
            }

            bool duplicado = await _context.Estudiantes
                .AnyAsync(e => e.Id != id && (e.Codigo == dto.Codigo || e.Correo == dto.Correo));

            if (duplicado)
            {
                throw new InvalidOperationException("Ya existe otro estudiante con ese codigo o correo, ingrese uno nuevo");
            }

            estudiante.Codigo = dto.Codigo;
            estudiante.Nombres = dto.Nombres;
            estudiante.Apellidos = dto.Apellidos;
            estudiante.Correo = dto.Correo;
            estudiante.Carrera = dto.Carrera;

            await _context.SaveChangesAsync();

            return estudiante;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if(estudiante == null)
            {
                return false;
            }

            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
