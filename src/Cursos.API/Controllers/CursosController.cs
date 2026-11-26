using Cursos.API.Dtos;
using Cursos.API.Models;
using Cursos.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cursos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursosController : ControllerBase
    {

        private readonly ICursoService _service;

        public CursosController(ICursoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Listar()
        {
            var cursos = await _service.ListarAsync();
            return Ok(cursos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> Obtener(int id)
        {
            var curso = await _service.ObtenerAsync(id);

            if (curso == null)
            {
                return NotFound();
            }

            return Ok(curso);
        }

        [HttpPost]
        public async Task<ActionResult<Curso>> Crear(CursoDto dto)
        {
            try
            {
                var curso = await _service.CrearAsync(dto);
                return CreatedAtAction(nameof(Obtener), new { id = curso.Id }, curso);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Curso>> Actualizar(int id, CursoDto dto)
        {
            try
            {
                var curso = await _service.ActualizarAsync(id, dto);

                if (curso == null)
                {
                    return NotFound();
                }

                return Ok(curso);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool eliminado = await _service.EliminarAsync(id);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
