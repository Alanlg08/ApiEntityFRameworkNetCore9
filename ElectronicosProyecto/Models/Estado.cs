using System;
using System.Collections.Generic;

namespace ElectronicosProyecto.Models;

public partial class Estado
{
    public int id { get; set; }

    public string nombre { get; set; } = null!;

    public string? descripcion { get; set; }

    public bool sis_status { get; set; }

    public DateTime? fecha_registro { get; set; }

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
}
