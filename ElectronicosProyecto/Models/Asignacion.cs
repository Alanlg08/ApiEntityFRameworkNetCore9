using System;
using System.Collections.Generic;

namespace ElectronicosProyecto.Models;

public partial class Asignacion
{
    public int id { get; set; }

    public int fk_equipo_id { get; set; }

    public string responsable { get; set; } = null!;

    public DateTime FechaAsignacion { get; set; }

    public DateTime? FechaLiberacion { get; set; }

    public string? descripcion { get; set; }

    public int fk_empresa_id { get; set; }

    public bool sis_status { get; set; }

    public DateTime? fecha_registro { get; set; }

    public virtual Empresa fk_empresa { get; set; } = null!;

    public virtual Equipo fk_equipo { get; set; } = null!;
}
