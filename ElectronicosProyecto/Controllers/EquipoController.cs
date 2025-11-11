using ElectronicosProyecto.DTOs.Equipo;
using ElectronicosProyecto.DTOs.Estado;
using ElectronicosProyecto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectronicosProyecto.Controllers
{
    public class EquipoController : ControllerBase
    {
        private readonly AppDbContext context;

        public EquipoController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("ObtenerEquipo")]
        public async Task<ActionResult<IEnumerable<EquipoDto>>> Get()
        {
            var equipo = await context.Equipos
                .AsNoTracking()
                .Select(e => new EquipoDto
                {
                    id = e.id,
                    numero_serie = e.num_serie,
                    descripcion = e.descripcion,
                    fk_empresa_id = e.fk_empresa_id,
                    nombre_empresa = e.fk_empresa.nombre,
                    fk_categoria_id = e.fk_categoria_id,
                    nombre_categoria = e.fk_categoria.nombre,
                    fk_ubicacion_id = e.fk_ubicacion_id,
                    nombre_ubicacion = e.fk_ubicacion.nombre,
                    fk_status_id = e.fk_status_id,
                    nombre_status = e.fk_status.nombre,
                    fecha_registro = e.fecha_registro
                })
                .ToListAsync();
            return Ok(equipo);
        }

        [HttpGet("ObtenerEquipo/{id:int}")]
        public async Task<ActionResult<EquipoDto>> GetById(int id)
        {
            var equipo = await context.Equipos
                .AsNoTracking()
                .Where(x => x.id == id)
                .Select(e => new EquipoDto
                {
                    id = e.id,
                    numero_serie = e.num_serie,
                    descripcion = e.descripcion,
                    fk_empresa_id = e.fk_empresa_id,
                    nombre_empresa = e.fk_empresa.nombre,
                    fk_categoria_id = e.fk_categoria_id,
                    nombre_categoria = e.fk_categoria.nombre,
                    fk_ubicacion_id = e.fk_ubicacion_id,
                    nombre_ubicacion = e.fk_ubicacion.nombre,
                    fk_status_id = e.fk_status_id,
                    nombre_status = e.fk_status.nombre,
                    fecha_registro = e.fecha_registro
                })
                .FirstOrDefaultAsync();
            return equipo is null ? NotFound() : Ok(equipo);
        }

        [HttpPost("AgregarEquipo")]
        public async Task<ActionResult> Post(EquipoCreateDto equipoDto)
        {
            var equipo = new Equipo {
                num_serie = equipoDto.NumSerie,
                descripcion = equipoDto.Descripcion,
                fk_empresa_id = equipoDto.FkEmpresaId,
                fk_categoria_id = equipoDto.FkCategoriaId,
                fk_ubicacion_id = equipoDto.FkUbicacionId,
                fk_status_id = equipoDto.FkStatusId,
                fecha_registro = DateTime.Now,
            };

            context.Add(equipo);
            await context.SaveChangesAsync();

            var result = new EquipoDto
            {
                id = equipo.id,
                numero_serie = equipo.num_serie,
                descripcion = equipo.descripcion,
                fk_empresa_id = equipo.fk_empresa_id,
                nombre_empresa = equipo.fk_empresa.nombre,
                fk_categoria_id = equipo.fk_categoria_id,
                nombre_categoria = equipo.fk_categoria.nombre,
                fk_ubicacion_id = equipo.fk_ubicacion_id,
                nombre_ubicacion = equipo.fk_ubicacion.nombre,
                fk_status_id = equipo.fk_status_id,
                nombre_status = equipo.fk_status.nombre,
                fecha_registro = equipo.fecha_registro
            };

            return CreatedAtAction(nameof(GetById), new { id = equipo.id }, result);
        }

        [HttpPut("EditarEquipo/{id:int}")]
        public async Task<ActionResult> Put(int id, EquipoUpdateDto equipoDto)
        {
            var equipo = await context.Equipos.FindAsync(id);
            if (equipo is null)
            {
                return NotFound();
            }

            equipo.num_serie = equipoDto.NumSerie;
            equipo.descripcion = equipoDto.Descripcion;
            equipo.fk_categoria_id = equipoDto.FkCategoriaId;
            equipo.fk_ubicacion_id = equipoDto.FkUbicacionId;
            equipo.fk_empresa_id = equipoDto.FkEmpresaId;
            equipo.fk_status_id = equipoDto.FkStatusId;

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("EliminarEquipo/{id:int}")]
        public async Task<ActionResult>Delete(int id)
        {
            var registrosBorrados = await context.Equipos.Where(x => x.id == id).ExecuteDeleteAsync();
            if (registrosBorrados == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
