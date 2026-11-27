using Estudiantes.API.Data;
using Estudiantes.API.Dtos;
using Estudiantes.API.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Estudiantes.Tests
{
    public class EstudianteServiceTests
    {
        private static EstudianteDbContext CrearContexto()
        {
            var options = new DbContextOptionsBuilder<EstudianteDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new EstudianteDbContext(options);
        }

        [Fact]
        public async Task CrearEstudiante_Ok()
        {
            using var context = CrearContexto();
            var service = new EstudianteService(context);
            var dto = new EstudianteDto
            {
                Codigo = "2026001",
                Nombres = "Ana",
                Apellidos = "Perez",
                Correo = "ana@uc.edu.pe",
                Carrera = "Ingenieria de Sistemas"
            };

            var estudiante = await service.CrearAsync(dto);

            Assert.True(estudiante.Id > 0);
            Assert.Equal("2026001", estudiante.Codigo);
            Assert.Single(await context.Estudiantes.ToListAsync());
        }

        [Fact]
        public async Task CrearEstudiante_Duplicado_Error()
        {
            using var context = CrearContexto();
            var service = new EstudianteService(context);
            var dto = new EstudianteDto
            {
                Codigo = "2026001",
                Nombres = "Ana",
                Apellidos = "Perez",
                Correo = "ana@uc.edu.pe",
                Carrera = "Ingenieria de Sistemas"
            };
            await service.CrearAsync(dto);

            var duplicado = new EstudianteDto
            {
                Codigo = "2026001",
                Nombres = "Luis",
                Apellidos = "Gomez",
                Correo = "luis@uc.edu.pe",
                Carrera = "Derecho"
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.CrearAsync(duplicado));
        }

        [Fact]
        public async Task ObtenerEstudiante_NoExiste()
        {
            using var context = CrearContexto();
            var service = new EstudianteService(context);

            var estudiante = await service.ObtenerAsync(999);

            Assert.Null(estudiante);
        }

        [Fact]
        public async Task ActualizarEstudiante_Ok()
        {
            using var context = CrearContexto();
            var service = new EstudianteService(context);
            var creado = await service.CrearAsync(new EstudianteDto
            {
                Codigo = "2026001",
                Nombres = "Ana",
                Apellidos = "Perez",
                Correo = "ana@uc.edu.pe",
                Carrera = "Sistemas"
            });

            var actualizado = await service.ActualizarAsync(creado.Id, new EstudianteDto
            {
                Codigo = "2026001",
                Nombres = "Ana Maria",
                Apellidos = "Perez",
                Correo = "ana@uc.edu.pe",
                Carrera = "Ingenieria de Software"
            });

            Assert.NotNull(actualizado);
            Assert.Equal("Ana Maria", actualizado.Nombres);
            Assert.Equal("Ingenieria de Software", actualizado.Carrera);
        }

        [Fact]
        public async Task EliminarEstudiante_Ok()
        {
            using var context = CrearContexto();
            var service = new EstudianteService(context);
            var creado = await service.CrearAsync(new EstudianteDto
            {
                Codigo = "2026001",
                Nombres = "Ana",
                Apellidos = "Perez",
                Correo = "ana@uc.edu.pe",
                Carrera = "Sistemas"
            });

            var eliminado = await service.EliminarAsync(creado.Id);

            Assert.True(eliminado);
            Assert.Empty(await context.Estudiantes.ToListAsync());
        }

    }
}
