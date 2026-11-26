using Estudiantes.API.Dtos;
using Estudiantes.API.Models;

namespace Estudiantes.API.Services
{
    public interface IEstudianteService
    {
        Task<List<Estudiante>> ListarAsync();
        Task<Estudiante?> ObtenerAsync(int id);
        Task<Estudiante> CrearAsync(EstudianteDto dto);
        Task<Estudiante?> ActualizarAsync(int id, EstudianteDto dto);
        Task<bool> EliminarAsync(int id);

    }
}
