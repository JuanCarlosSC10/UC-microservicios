using Estudiantes.API.Dtos;
using Estudiantes.API.Models;
using Estudiantes.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Estudiantes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {

        private readonly IEstudianteService _service;

        public EstudiantesController(IEstudianteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Estudiante>>> Listar()
        {
            var estudiantes = await _service.ListarAsync();
            return Ok(estudiantes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Estudiante>> Obtener(int id)
        {
            var estudiante = await _service.ObtenerAsync(id);

            if (estudiante == null)
            {
                return NotFound();
            }

            return Ok(estudiante);
        }

        [HttpPost]
        public async Task<ActionResult<Estudiante>> Crear(EstudianteDto dto)
        {
            try
            {
                var estudiante = await _service.CrearAsync(dto);
                return CreatedAtAction(nameof(Obtener), new { id = estudiante.Id }, estudiante);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Estudiante>> Actualizar(int id, EstudianteDto dto)
        {
            try
            {
                var estudiante = await _service.ActualizarAsync(id, dto);

                if (estudiante == null)
                {
                    return NotFound();
                }

                return Ok(estudiante);
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
