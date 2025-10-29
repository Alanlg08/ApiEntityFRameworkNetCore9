using ElectronicosProyecto.DTOs.Categoria;
using ElectronicosProyecto.DTOs.Empresa;
using ElectronicosProyecto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectronicosProyecto.Controllers
{
    public class CategoriaController: ControllerBase
    {
        private readonly AppDbContext context;

        public CategoriaController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("ConsultarCategorias")]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> Get()
        {
            var Categorias = await context.Categoria
                .AsNoTracking()
                .Include(x => x.fk_empresa_id)
                .Select(x => new CategoriaDto
                {
                    id = x.id,
                    nombre = x.nombre,
                    fk_empresa_id = x.fk_empresa_id,
                    sis_status = x.sis_status,
                    fecha_registro = x.fecha_registro,

                    Empresa = new EmpresaDto
                    {
                        Id = x.id,
                        Nombre = x.nombre,
                    }
                })
                .ToListAsync();
            return Ok(Categorias);
        }

        [HttpGet("ObtenerCategoria/{id:int}")]
        public async Task<ActionResult<CategoriaDto>> GetById(int id)
        {
            var ObtenerCategoria = await context.Categoria
                .AsNoTracking()
                .Include(x => x.fk_empresa_id)
                .Where(x => x.id == id)
                .Select(x => new CategoriaDto
                {
                    id = x.id,
                    nombre = x.nombre,
                    fk_empresa_id = x.fk_empresa_id,
                    sis_status = x.sis_status,
                    fecha_registro = x.fecha_registro,

                    Empresa = new EmpresaDto
                    {
                        Id= x.id,
                        Nombre= x.nombre
                    }
                })
                .FirstOrDefaultAsync();
            return Ok(ObtenerCategoria);
        }

        [HttpPost("CrearCategoria")]
        public async Task<ActionResult> Post(CategoriaCreateDto categoriaDto)
        {
            var nuevoRegistro = new Categorium
            {
                nombre = categoriaDto.Nombre,
                fk_empresa_id = categoriaDto.FkEmpresaId,
                fecha_registro = DateTime.Now
            };
            context.Add(nuevoRegistro);
            await context.SaveChangesAsync();
            var result = new CategoriaDto
            {
                id = nuevoRegistro.id,
                nombre = nuevoRegistro.nombre,
                fk_empresa_id = nuevoRegistro.fk_empresa_id,
                fecha_registro = nuevoRegistro.fecha_registro,
                sis_status = nuevoRegistro.sis_status,

                Empresa = new EmpresaDto
                {
                    Id = nuevoRegistro.id,
                    Nombre = nuevoRegistro.nombre,
                }
            };
            return CreatedAtAction(nameof(GetById), new { id = nuevoRegistro.id }, result);
        }

        [HttpPut("EditarCategoria/{id:int}")]
        public async Task<ActionResult> Put(int id, CategoriaUpdateDto categoriaDto)
        {
            var editarCategoria = await context.Categoria.FindAsync(id);
            if (editarCategoria is null)
            {
                return NotFound();
            }

            editarCategoria.nombre = categoriaDto.Nombre;
            editarCategoria.fk_empresa_id = categoriaDto.FkEmpresaId;
            editarCategoria.sis_status = categoriaDto.Status;

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("EliminarCategoria/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var registrosBorrados = await context.Categoria.Where(x => x.id == id).ExecuteDeleteAsync();
            if (registrosBorrados == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
