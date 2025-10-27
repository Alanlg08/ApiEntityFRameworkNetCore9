using System;
using System.Collections.Generic;

namespace ElectronicosProyecto.Models;

public partial class MovimientoEquipo
{
    public int id { get; set; }

    public string tipo_movimiento { get; set; } = null!;

    public int fk_equipo_id { get; set; }

    public int? fk_ubicacion_origen_id { get; set; }

    public int fk_ubicacion_final_id { get; set; }

    public string? descripcion { get; set; }

    public string responsable { get; set; } = null!;

    public int fk_empresa_id { get; set; }

    public DateTime? fecha_registro { get; set; }

    public virtual Empresa fk_empresa { get; set; } = null!;

    public virtual Equipo fk_equipo { get; set; } = null!;

    public virtual Ubicacion fk_ubicacion_final { get; set; } = null!;

    public virtual Ubicacion? fk_ubicacion_origen { get; set; }
}
