using System.ComponentModel.DataAnnotations;

namespace ElectronicosProyecto.DTOs.Empresa
{
    public class EmpresaCreateDto
    {
        [Required]
        [MaxLength(120)]
        public string Nombre { get; set; } = string.Empty;
    }
}
