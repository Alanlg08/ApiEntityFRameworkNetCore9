using System.ComponentModel.DataAnnotations;

namespace ElectronicosProyecto.DTOs.Equipo
{
    public class EquipoCreateDto
    {
        [MaxLength(20, ErrorMessage = "El campo {0} solo debe ser de {1} carcteres o menos")]
        public string NumSerie { get; set; } = string.Empty;
        [MaxLength(180, ErrorMessage = "El campo {0} solo debe ser de {1} carcteres o menos")]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int FkEmpresaId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int FkCategoriaId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int FkUbicacionId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int FkStatusId { get; set; }
    }
}
