using System.ComponentModel.DataAnnotations;

namespace ElectronicosProyecto.DTOs.Estado
{
    public class EstadoUpdateDto
    {
        [Required]
        [MaxLength(120)]
        public string Nombre { get; set; } = string.Empty;
        [MaxLength(180)]
        public string? Descripcion { get; set; }

        public bool Status { get; set; } = true;
    }
}
