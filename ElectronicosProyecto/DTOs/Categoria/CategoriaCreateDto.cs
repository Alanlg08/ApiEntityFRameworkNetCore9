using System.ComponentModel.DataAnnotations;

namespace ElectronicosProyecto.DTOs.Categoria
{
    public class CategoriaCreateDto
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; } = null!;
        [Required]
        public int FkEmpresaId { get; set; }
    }
}
