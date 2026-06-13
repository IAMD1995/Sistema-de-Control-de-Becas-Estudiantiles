using Microsoft.AspNetCore.Mvc;
using SistemaControlBecas.Api.Data;
using SistemaControlBecas.Api.Models.Dtos;
using SistemaControlBecas.Api.Models.Entities;

namespace SistemaControlBecas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EstudiantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/estudiantes
        [HttpGet]
        public ActionResult<List<EstudianteDTO>> GetAll()
        {
            var estudiantes = _context.Estudiantes.ToList();

            var estudianteDTOs = estudiantes.Select(e => new EstudianteDTO
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Matricula = e.Matricula,
                Promedio = e.Promedio,
                EstaActivo = e.EstaActivo
            }).ToList();

            return Ok(estudianteDTOs);
        }

        // GET: api/estudiantes/{id}
        [HttpGet("{id}")]
        public ActionResult<EstudianteDTO> GetById(int id)
        {
            var estudiante = _context.Estudiantes.Find(id);
            if (estudiante == null)
                return NotFound(new { mensaje = $"No se encontró el estudiante con Id {id}." });

            var estudianteDTO = new EstudianteDTO
            {
                Id = estudiante.Id,
                Nombre = estudiante.Nombre,
                Matricula = estudiante.Matricula,
                Promedio = estudiante.Promedio,
                EstaActivo = estudiante.EstaActivo
            };

            return Ok(estudianteDTO);
        }

        // POST: api/estudiantes
        [HttpPost]
        public ActionResult<EstudianteDTO> Create([FromBody] CreateEstudianteDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre))
                return BadRequest(new { mensaje = "El nombre del estudiante es obligatorio." });

            if (string.IsNullOrWhiteSpace(request.Matricula))
                return BadRequest(new { mensaje = "La matrícula es obligatoria." });

            var newEstudiante = new Estudiante
            {
                Nombre = request.Nombre,
                Matricula = request.Matricula,
                Promedio = request.Promedio,
                EstaActivo = true
            };

            _context.Estudiantes.Add(newEstudiante);
            _context.SaveChanges();

            var estudianteDTO = new EstudianteDTO
            {
                Id = newEstudiante.Id,
                Nombre = newEstudiante.Nombre,
                Matricula = newEstudiante.Matricula,
                Promedio = newEstudiante.Promedio,
                EstaActivo = newEstudiante.EstaActivo
            };

            return CreatedAtAction(nameof(GetById), new { id = newEstudiante.Id }, estudianteDTO);
        }

        // PUT: api/estudiantes/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CreateEstudianteDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre))
                return BadRequest(new { mensaje = "El nombre del estudiante es obligatorio." });

            if (string.IsNullOrWhiteSpace(request.Matricula))
                return BadRequest(new { mensaje = "La matrícula es obligatoria." });

            var existingEstudiante = _context.Estudiantes.Find(id);
            if (existingEstudiante == null)
                return NotFound(new { mensaje = $"No se encontró el estudiante con Id {id}." });

            existingEstudiante.Nombre = request.Nombre;
            existingEstudiante.Matricula = request.Matricula;
            existingEstudiante.Promedio = request.Promedio;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/estudiantes/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var estudiante = _context.Estudiantes.Find(id);
            if (estudiante == null)
                return NotFound(new { mensaje = $"No se encontró el estudiante con Id {id}." });

            bool tieneSolicitudes = _context.Solicitudes.Any(s => s.EstudianteId == id);
            if (tieneSolicitudes)
                return BadRequest(new { mensaje = "No se puede eliminar el estudiante porque tiene solicitudes asociadas." });

            _context.Estudiantes.Remove(estudiante);
            _context.SaveChanges();

            return NoContent();
        }
    }
}