using System.ComponentModel.DataAnnotations;
namespace Cursos.API.Dtos
{
    public class CursoDto
    {
        [Required, MaxLength(20)]
        public string Codigo { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public string Nombre {  get; set; } = string.Empty;

        [Required, Range(1, 20)]
        public int Creditos { get; set; }

        [Required, MaxLength(120)]
        public string Docente { get; set; } = string.Empty;

    }
}
