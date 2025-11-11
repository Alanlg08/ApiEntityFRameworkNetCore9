using System.ComponentModel.DataAnnotations;

namespace ElectronicosProyecto.DTOs.Estado
{
    public class EstadoUpdateDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(120, ErrorMessage = "El campo {0} solo debe ser de {1} carcteres o menos")]
        public string Nombre { get; set; } = string.Empty;
        [MaxLength(180, ErrorMessage = "El campo {0} solo debe ser de {1} carcteres o menos")]
        public string? Descripcion { get; set; }

        public bool Status { get; set; } = true;
    }
}
