using System.ComponentModel.DataAnnotations;

namespace ElectronicosProyecto.DTOs.Ubicacion
{
    public class UbicacionUpdateDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(120)]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int FkEmpresaId { get; set; }

        public bool Status { get; set; } = true;
    }
}
