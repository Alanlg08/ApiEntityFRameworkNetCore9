using System.ComponentModel.DataAnnotations;

namespace ElectronicosProyecto.DTOs.Categoria
{
    public class CategoriaUpdateDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(120, ErrorMessage = "El campo {0} solo debe ser de {1} carcteres o menos")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int FkEmpresaId { get; set; }

        public bool Status { get; set; } = true;
    }
}
