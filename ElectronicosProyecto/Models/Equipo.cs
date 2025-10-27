using System;
using System.Collections.Generic;

namespace ElectronicosProyecto.Models;

public partial class Equipo
{
    public int id { get; set; }

    public string num_serie { get; set; } = null!;

    public string? descripcion { get; set; }

    public int fk_empresa_id { get; set; }

    public int fk_categoria_id { get; set; }

    public int fk_ubicacion_id { get; set; }

    public int fk_status_id { get; set; }

    public byte[] rowver { get; set; } = null!;

    public DateTime? fecha_registro { get; set; }

    public virtual ICollection<Asignacion> Asignacions { get; set; } = new List<Asignacion>();

    public virtual ICollection<MovimientoEquipo> MovimientoEquipos { get; set; } = new List<MovimientoEquipo>();

    public virtual Categorium fk_categoria { get; set; } = null!;

    public virtual Empresa fk_empresa { get; set; } = null!;

    public virtual Estado fk_status { get; set; } = null!;

    public virtual Ubicacion fk_ubicacion { get; set; } = null!;
}
