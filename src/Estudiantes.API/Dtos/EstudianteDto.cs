using System.ComponentModel.DataAnnotations;

namespace Estudiantes.API.Dtos
{
    public class EstudianteDto
    {
        [Required, MaxLength(20)]
        public string Codigo { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(150)]
        public string Correo { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Carrera {  get; set; } = string.Empty;

    }
}
