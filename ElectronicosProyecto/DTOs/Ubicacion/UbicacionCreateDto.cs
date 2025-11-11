using System.ComponentModel.DataAnnotations;

namespace ElectronicosProyecto.DTOs.Ubicacion
{
    public class UbicacionCreateDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(120, ErrorMessage = "El campo {0} solo debe ser de {1} carcteres o menos")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int FkEmpresaId { get; set; }
    }
}
