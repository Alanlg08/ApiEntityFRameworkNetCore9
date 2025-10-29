using System.ComponentModel.DataAnnotations;

namespace ElectronicosProyecto.DTOs.Ubicacion
{
    public class UbicacionCreateDto
    {
        [Required]
        [MaxLength(120)]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        public int FkEmpresaId { get; set; }
    }
}
