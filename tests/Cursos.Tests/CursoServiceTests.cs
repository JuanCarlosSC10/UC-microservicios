using Cursos.API.Data;
using Cursos.API.Dtos;
using Cursos.API.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cursos.Tests
{
    public class CursoServiceTests
    {

        private static CursoDbContext CrearContexto()
        {
            var options = new DbContextOptionsBuilder<CursoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new CursoDbContext(options);
        }

        [Fact]
        public async Task CrearCurso_Ok()
        {
            using var context = CrearContexto();
            var service = new CursoService(context);
            var dto = new CursoDto
            {
                Codigo = "SIS101",
                Nombre = "Algoritmos y Estructura de Datos",
                Creditos = 4,
                Docente = "Carlos Mendoza"
            };

            var curso = await service.CrearAsync(dto);

            Assert.True(curso.Id > 0);
            Assert.Equal("SIS101", curso.Codigo);
            Assert.Equal(4, curso.Creditos);
            Assert.Single(await context.Cursos.ToListAsync());
        }

        [Fact]
        public async Task CrearCurso_Duplicado_Error()
        {
            using var context = CrearContexto();
            var service = new CursoService(context);
            var dto = new CursoDto
            {
                Codigo = "SIS101",
                Nombre = "Algoritmos y Estructura de Datos",
                Creditos = 4,
                Docente = "Carlos Mendoza"
            };
            await service.CrearAsync(dto);

            var duplicado = new CursoDto
            {
                Codigo = "SIS101",
                Nombre = "Bases de Datos",
                Creditos = 3,
                Docente = "Elena Rojas"
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.CrearAsync(duplicado));
        }

        [Fact]
        public async Task ObtenerCurso_NoExiste()
        {
            using var context = CrearContexto();
            var service = new CursoService(context);

            var curso = await service.ObtenerAsync(999);

            Assert.Null(curso);
        }

        [Fact]
        public async Task ActualizarCurso_Ok()
        {
            using var context = CrearContexto();
            var service = new CursoService(context);
            var creado = await service.CrearAsync(new CursoDto
            {
                Codigo = "SIS101",
                Nombre = "Algoritmos",
                Creditos = 4,
                Docente = "Carlos Mendoza"
            });

            var actualizado = await service.ActualizarAsync(creado.Id, new CursoDto
            {
                Codigo = "SIS101",
                Nombre = "Algoritmos Avanzados",
                Creditos = 5,
                Docente = "Carlos Mendoza"
            });

            Assert.NotNull(actualizado);
            Assert.Equal("Algoritmos Avanzados", actualizado.Nombre);
            Assert.Equal(5, actualizado.Creditos);
        }

        [Fact]
        public async Task EliminarCurso_Ok()
        {
            using var context = CrearContexto();
            var service = new CursoService(context);
            var creado = await service.CrearAsync(new CursoDto
            {
                Codigo = "SIS101",
                Nombre = "Algoritmos",
                Creditos = 4,
                Docente = "Carlos Mendoza"
            });

            var eliminado = await service.EliminarAsync(creado.Id);

            Assert.True(eliminado);
            Assert.Empty(await context.Cursos.ToListAsync());
        }

    }
}
