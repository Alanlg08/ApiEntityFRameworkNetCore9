using System;
using System.Collections.Generic;

namespace ElectronicosProyecto.Models;

public partial class Ubicacion
{
    public int id { get; set; }

    public string nombre { get; set; } = null!;

    public int fk_empresa_id { get; set; }

    public bool sis_status { get; set; }

    public DateTime? fecha_registro { get; set; }

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();

    public virtual ICollection<MovimientoEquipo> MovimientoEquipofk_ubicacion_finals { get; set; } = new List<MovimientoEquipo>();

    public virtual ICollection<MovimientoEquipo> MovimientoEquipofk_ubicacion_origens { get; set; } = new List<MovimientoEquipo>();

    public virtual Empresa fk_empresa { get; set; } = null!;
}
