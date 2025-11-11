namespace ElectronicosProyecto.DTOs.Equipo
{
    public class EquipoDto
    {
        public int id { get; set; }
        public string numero_serie { get; set; } = string.Empty;
        public string? descripcion { get; set; }
        public int fk_empresa_id { get; set; }
        public string nombre_empresa { get; set; } = string.Empty;
        public int fk_categoria_id { get; set; }
        public string nombre_categoria { get; set; } = string.Empty;
        public int fk_ubicacion_id { get; set; }
        public string nombre_ubicacion { get; set; } = string.Empty;
        public int fk_status_id { get; set; }
        public string nombre_status { get; set; } = string.Empty;
        public DateTime? fecha_registro { get; set; }
        public string RowVersion { get; set; } = string.Empty; // Base64
    }
}
