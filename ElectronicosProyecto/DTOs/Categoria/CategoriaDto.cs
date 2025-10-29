using ElectronicosProyecto.DTOs.Empresa;
namespace ElectronicosProyecto.DTOs.Categoria
{
    public class CategoriaDto
    {
        public int id { get; set; }

        public string nombre { get; set; } = string.Empty;

        public int fk_empresa_id { get; set; }

        public bool sis_status { get; set; }

        public DateTime? fecha_registro { get; set; }

        public EmpresaDto Empresa { get; set; } = null;

    }
}
