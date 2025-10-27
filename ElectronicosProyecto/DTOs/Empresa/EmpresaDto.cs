namespace ElectronicosProyecto.DTOs.Empresa
{
    public class EmpresaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Activa { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
