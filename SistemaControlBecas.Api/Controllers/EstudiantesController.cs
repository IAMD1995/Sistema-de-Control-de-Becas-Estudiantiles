using Microsoft.AspNetCore.Mvc;
using SistemaControlBecas.Api.Models.Entities;

namespace SistemaControlBecas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private static List<Estudiante> _estudiantes = new();
        private static int _nextId = 1;

        [HttpGet]
        public IActionResult GetAll() => Ok(_estudiantes);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var est = _estudiantes.FirstOrDefault(e => e.Id == id);
            return est is null ? NotFound(new { mensaje = $"No se encontró el estudiante con Id {id}." }) : Ok(est);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Estudiante estudiante)
        {
            estudiante.Id = _nextId++;
            _estudiantes.Add(estudiante);
            return CreatedAtAction(nameof(GetById), new { id = estudiante.Id }, estudiante);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Estudiante actualizado)
        {
            var est = _estudiantes.FirstOrDefault(e => e.Id == id);
            if (est is null) return NotFound(new { mensaje = $"No se encontró el estudiante con Id {id}." });
            est.Nombre = actualizado.Nombre;
            est.Matricula = actualizado.Matricula;
            est.Promedio = actualizado.Promedio;
            est.EstaActivo = actualizado.EstaActivo;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var est = _estudiantes.FirstOrDefault(e => e.Id == id);
            if (est is null) return NotFound(new { mensaje = $"No se encontró el estudiante con Id {id}." });
            _estudiantes.Remove(est);
            return NoContent();
        }
    }
}