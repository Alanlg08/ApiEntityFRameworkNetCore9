using System;
using System.Collections.Generic;

namespace ElectronicosProyecto.Models;

public partial class Empresa
{
    public int id { get; set; }

    public string nombre { get; set; } = null!;

    public bool sis_status { get; set; }

    public DateTime? fecha_registro { get; set; }

    public virtual ICollection<Asignacion> Asignacions { get; set; } = new List<Asignacion>();

    public virtual ICollection<Categorium> Categoria { get; set; } = new List<Categorium>();

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();

    public virtual ICollection<MovimientoEquipo> MovimientoEquipos { get; set; } = new List<MovimientoEquipo>();

    public virtual ICollection<Ubicacion> Ubicacions { get; set; } = new List<Ubicacion>();
}
