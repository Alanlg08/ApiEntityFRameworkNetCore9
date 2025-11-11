using System.ComponentModel.DataAnnotations;

namespace ElectronicosProyecto.DTOs.Empresa
{
    public class EmpresaDtoPatch
    {
        [MaxLength(120, ErrorMessage = "El campo {0} solo debe ser de {1} carcteres o menos")]
        public string Nombre { get; set; } = string.Empty;
        public bool? Status { get; set; }

    }
}
