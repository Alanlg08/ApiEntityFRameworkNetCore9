using System.ComponentModel.DataAnnotations;

namespace ElectronicosProyecto.DTOs.Empresa
{
    public class EmpresaUpdateDto
    {
        [Required]
        [MaxLength(120)]
        public string Nombre { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
