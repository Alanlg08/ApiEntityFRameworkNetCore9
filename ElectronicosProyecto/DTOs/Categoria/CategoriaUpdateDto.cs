using System.ComponentModel.DataAnnotations;

namespace ElectronicosProyecto.DTOs.Categoria
{
    public class CategoriaUpdateDto
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; } = null!;
        [Required]
        public int FkEmpresaId { get; set; }

        public bool Status { get; set; } = true;
    }
}
