using Cursos.API.Models;
using Cursos.API.Dtos;

namespace Cursos.API.Services
{
    public interface ICursoService
    {
        Task<List<Curso>> ListarAsync();
        Task<Curso?> ObtenerAsync(int id);
        Task<Curso> CrearAsync(CursoDto dto);
        Task<Curso?> ActualizarAsync(int id, CursoDto dto);
        Task<bool> EliminarAsync(int id);

    }
}
