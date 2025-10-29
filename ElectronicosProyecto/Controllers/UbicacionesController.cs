using ElectronicosProyecto.DTOs.Empresa;
using ElectronicosProyecto.DTOs.Ubicacion;
using ElectronicosProyecto.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectronicosProyecto.Controllers
{
    [ApiController]
    [Route("api/ubicaciones")]
    public class UbicacionesController: ControllerBase
    {
        private readonly AppDbContext context;

        public UbicacionesController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("ObtenerUbicaciones")]
        public async Task<ActionResult<IEnumerable<UbicacionDto>>> Get()
        {
            var ubicacion = await context.Ubicacion
                .AsNoTracking()
                .Include(e => e.fk_empresa_id)
                .Select(e => new UbicacionDto
                {
                    id = e.id,
                    nombre = e.nombre,
                    fk_empresa_id = e.fk_empresa_id,
                    sis_status = e.sis_status,
                    fecha_registro = e.fecha_registro,

                    Empresa = new EmpresaDto
                     {
                        Id = e.id,
                        Nombre = e.nombre
                     }
                })
                .ToListAsync();
            return Ok(ubicacion);
        }

        [HttpGet("ObtenerUbicaciones/{id:int}")]
        public async Task<ActionResult<UbicacionDto>> GetById(int id)
        {
            var ubicacion = await context.Ubicacion
                .AsNoTracking()
                .Include(e => e.fk_empresa_id)
                .Where(e => e.id == id)
                .Select(e => new UbicacionDto
                {
                    id = e.id,
                    nombre = e.nombre,
                    fk_empresa_id = e.fk_empresa_id,
                    sis_status = e.sis_status,
                    fecha_registro = e.fecha_registro,

                    Empresa = new EmpresaDto
                    {
                        Id = e.id,
                        Nombre = e.nombre
                    }
                })
                .FirstOrDefaultAsync();
            return Ok(ubicacion);
        }

        [HttpPost("AgregarUbicacion")]
        public async Task<ActionResult> Post(UbicacionCreateDto ubicacionDto)
        {
            var ubicacion = new Ubicacion
            {
                nombre = ubicacionDto.Nombre,
                fk_empresa_id = ubicacionDto.FkEmpresaId,
                fecha_registro = DateTime.Now
            };
            context.Add(ubicacion);
            await context.SaveChangesAsync();

            var result = new UbicacionDto
            {
                id = ubicacion.id,
                nombre = ubicacion.nombre,
                fk_empresa_id = ubicacion.fk_empresa_id,
                sis_status = ubicacion.sis_status,
                fecha_registro = ubicacion.fecha_registro,

                Empresa = new EmpresaDto
                {
                    Id = ubicacion.id,
                    Nombre = ubicacion.nombre
                }
            };

            return CreatedAtAction(nameof(GetById), new { id = ubicacion.id }, result);

        }

        [HttpPut("EditarUbicacion/{id:int}")]
        public async Task<ActionResult> Put(int id, UbicacionUpdateDto ubicacionDto)
        {
            var actualizarUbicacion = await context.Ubicacion.FindAsync(id);
            if (actualizarUbicacion is null)
            {
                return NotFound();
            }

            actualizarUbicacion.nombre = ubicacionDto.Nombre;
            actualizarUbicacion.fk_empresa_id = ubicacionDto.FkEmpresaId;
            actualizarUbicacion.sis_status = ubicacionDto.Status;

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("EliminarUbicacion/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var registrosBorrados = await context.Ubicacion.Where(x => x.id == id).ExecuteDeleteAsync();
            if (registrosBorrados == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
