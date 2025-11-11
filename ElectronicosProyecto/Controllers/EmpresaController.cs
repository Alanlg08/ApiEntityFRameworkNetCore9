using Microsoft.AspNetCore.Mvc;
using ElectronicosProyecto.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using ElectronicosProyecto.DTOs.Empresa;
using Azure;
using ElectronicosProyecto.DTOs.Estado;
using Microsoft.AspNetCore.JsonPatch;

namespace ElectronicosProyecto.Controllers
{
    [ApiController]
    [Route("api/empresa")]
    public class EmpresaController: ControllerBase
    {
        private readonly AppDbContext context;

        public EmpresaController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("ObtenerEmpresas")]
        public async Task<ActionResult<IEnumerable<EmpresaDto>>> Get()
        {

            var empresas = await context.Empresas
            .AsNoTracking()
            .Select(e => new EmpresaDto
            {
                Id = e.id,
                Nombre = e.nombre,
                Activa = e.sis_status,
                FechaRegistro = e.fecha_registro
            })
            .ToListAsync();

            return Ok(empresas);

        }

        [HttpGet("ObtenerEmpresaId/{id:int}")]
        public async Task<ActionResult<EmpresaDto>> GetById(int id)
        {
            var empresa = await context.Empresas
            .AsNoTracking()
            .Where(e => e.id == id)      
            .Select(e => new EmpresaDto
            {
                Id = e.id,
                Nombre = e.nombre,
                Activa = e.sis_status,
                FechaRegistro = e.fecha_registro
            })
            .FirstOrDefaultAsync();

            //if (empresa is null)
            //{
            //    return NotFound();
            //}
            //return Ok(empresa);

            return empresa is null ? NotFound() : Ok(empresa);
        }

        [HttpPost("AgregarEmpresa")]
        public async Task<ActionResult> Post(EmpresaCreateDto empresa)
        {
            var nueva = new Empresa
            {
                nombre = empresa.Nombre,
                sis_status = true,
                fecha_registro = DateTime.Now
            };

            context.Empresas.Add(nueva);
            await context.SaveChangesAsync();

            //return Ok();
            //Devuelve el recurso creado
            //return CreatedAtAction(nameof(GetById), new { id = empresa.id }, empresa);

            var result = new EmpresaDto
            {
                Id = nueva.id,
                Nombre = nueva.nombre,
                Activa = nueva.sis_status,
                FechaRegistro = nueva.fecha_registro
            };

            return CreatedAtAction(nameof(GetById), new { id = nueva.id }, result);
        }

        [HttpPut("EditarEmpresa/{id:int}")]
        public async Task<ActionResult> Put(int id, EmpresaUpdateDto empresa)
        {

            var ActualizarEmpresa = await context.Empresas.FindAsync(id);
            if (ActualizarEmpresa is null)
                return NotFound();

            ActualizarEmpresa.nombre = empresa.Nombre;
            ActualizarEmpresa.sis_status = empresa.Status;

            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("EliminarEmpresa/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var registrosBorrados = await context.Empresas.Where(x => x.id == id).ExecuteDeleteAsync();
            if (registrosBorrados == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPatch("EdicionParcialEmpresa/{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<EmpresaDtoPatch> empresaDtoPatch)
        {
            if (empresaDtoPatch is null)
            {
                return BadRequest(); 
            }

            var empresaDB = await context.Empresas.FirstOrDefaultAsync(x => x.id == id);

            if (empresaDB is null)
            {
                return BadRequest();
            }

            var dto = new EmpresaDtoPatch
            {
                Nombre = empresaDB.nombre,
                Status = empresaDB.sis_status
            };

            empresaDtoPatch.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Aplica solo si vinieron valores (parcial)
            if (dto.Nombre is not null) empresaDB.nombre = dto.Nombre;
            if (dto.Status.HasValue) empresaDB.sis_status = dto.Status.Value;

            await context.SaveChangesAsync();
            return NoContent();

        }
    }
}
