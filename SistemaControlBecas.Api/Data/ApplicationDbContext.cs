using Microsoft.EntityFrameworkCore;
using SistemaControlBecas.Api.Models.Entities;
using System.Collections.Generic;

namespace SistemaControlBecas.Api.Data
{
    /// <summary>
    /// DbContext de la aplicación: maneja la conexión a la base de datos y el mapeo de entidades.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; } = null!;
        public DbSet<Beca> Becas { get; set; } = null!;
        public DbSet<Solicitud> Solicitudes { get; set; } = null!;
    }
}