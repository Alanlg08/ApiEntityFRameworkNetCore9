using System.Text.Json.Serialization;
using ElectronicosProyecto.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;


namespace ElectronicosProyecto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //ignorar ciclos
            builder.Services.AddControllers()
                .AddJsonOptions(op =>
                {
                    op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    // opcional:
                    // op.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                })
                .AddNewtonsoftJson();

            // EF Core Coneccion a BD
            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer("name=DefaultConnection")); // o GetConnectionString("DefaultConnection")

            // Swagger
            //expone los endpoints de tus controladores para que Swagger los lea
            builder.Services.AddEndpointsApiExplorer();
            //genera el documento JSON(swagger.json) y la interfaz interactiva / swagger
            builder.Services.AddSwaggerGen();

            // CORS
            //Permite que aplicaciones externas(como React, Angular o Postman) puedan consumir tu API sin bloqueos del navegador.
            builder.Services.AddCors(options =>
            {
                //AllowAnyOrigin() => cualquier dominio.
                //AllowAnyMethod() => cualquier verbo HTTP(GET, POST, PUT, DELETE...).
                //AllowAnyHeader() => cualquier encabezado personalizado.
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            var app = builder.Build();

            //Genera el documento OpenAPI(/ swagger / v1 / swagger.json).
            app.UseSwagger();
            //crea la interfaz visual para probar los endpoints directamente desde el navegador.
            app.UseSwaggerUI();
            //Redirige automáticamente HTTP => HTTPS.
            app.UseHttpsRedirection();
            //Aplica la política de CORS
            app.UseCors("AllowAll");
            //Middleware de autorización (para cuando se utiliza autenticacion)
            app.UseAuthorization();
            //Conecta los controladores al pipeline (Sin esto, tus rutas ([Route("api/empresa")])
            app.MapControllers();   
            //Ejecuta la aplicacion
            app.Run();
        }
    }
}
