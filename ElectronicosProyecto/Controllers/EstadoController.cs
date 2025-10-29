using ElectronicosProyecto.DTOs.Empresa;
using ElectronicosProyecto.DTOs.Estado;
using ElectronicosProyecto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectronicosProyecto.Controllers
{
    [ApiController]
    [Route("api/estado")]
    public class EstadoController: ControllerBase
    {
        private readonly AppDbContext context;

        public EstadoController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("ObtenerEstados")]
        public async Task<ActionResult<IEnumerable<EstadoDto>>> Get()
        {
            var estados = await context.Estados
                .AsNoTracking()
                .Select(e => new EstadoDto
                {
                    Id = e.id,
                    Nombre = e.nombre,
                    Descripcion = e.descripcion,
                    Status = e.sis_status,
                    FechaRegistro = e.fecha_registro
                })
                .ToListAsync();

            return Ok(estados);
        }

        [HttpGet("ObtenerEstado/{id:int}")]
        public async Task<ActionResult<EstadoDto>> GetById(int id)
        {
            var estado = await context.Estados
                .AsNoTracking()
                .Where(e => e.id == id)
                .Select(e => new EstadoDto
                {
                    Id = e.id,
                    Nombre = e.nombre,
                    Descripcion = e.descripcion,
                    Status = e.sis_status,
                    FechaRegistro = e.fecha_registro
                })
                .FirstOrDefaultAsync();

            return estado is null ? NotFound() : Ok(estado);
        }

        [HttpPost("AgregarEstado")]
        public async Task<ActionResult> Post(EstadoCreateDto estado)
        {
            var nuevo = new Estado
            {
                nombre = estado.Nombre,
                descripcion = estado.Descripcion,
                fecha_registro = DateTime.Now,
            };
            context.Add(nuevo);
            await context.SaveChangesAsync();

            var result = new EstadoDto
            {
                Id = nuevo.id,
                Nombre = nuevo.nombre,
                Descripcion = nuevo.descripcion,
                FechaRegistro = nuevo.fecha_registro,
                Status = nuevo.sis_status
            };

            return CreatedAtAction(nameof(GetById), new { id = nuevo.id }, result);

        }

        [HttpPut("EditarEstado/{id:int}")]
        public async Task<ActionResult> Put(int id, EstadoUpdateDto estado)
        {
            var actulizar = await context.Estados.FindAsync(id);
            if (actulizar is null)
            {
                return NotFound();
            }

            actulizar.nombre = estado.Nombre;
            actulizar.descripcion = estado.Descripcion;
            actulizar.sis_status = estado.Status;

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("EliminarEstado/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var registrosBorrados = await context.Estados.Where(x => x.id == id).ExecuteDeleteAsync();
            if (registrosBorrados == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
