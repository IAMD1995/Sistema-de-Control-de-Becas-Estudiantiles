using Microsoft.AspNetCore.Mvc;
using SistemaControlBecas.Api.Models.Entities;
using SistemaControlBecas.Api.Models.Enums;

namespace SistemaControlBecas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitudesController : ControllerBase
    {
        private static List<Solicitud> _solicitudes = new();
        private static int _nextId = 1;

        [HttpGet]
        public IActionResult GetAll() => Ok(_solicitudes);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var solicitud = _solicitudes.FirstOrDefault(s => s.Id == id);
            if (solicitud is null) return NotFound(new { mensaje = $"No se encontró la solicitud con Id {id}." });
            return Ok(solicitud);
        }

        [HttpGet("estudiante/{estudianteId}")]
        public IActionResult GetByEstudiante(int estudianteId)
        {
            var solicitudes = _solicitudes.Where(s => s.EstudianteId == estudianteId).ToList();
            return Ok(solicitudes);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Solicitud solicitud)
        {
            bool yaExiste = _solicitudes.Any(s => s.EstudianteId == solicitud.EstudianteId && s.BecaId == solicitud.BecaId);
            if (yaExiste) return Conflict(new { mensaje = $"El estudiante con Id {solicitud.EstudianteId} ya tiene una solicitud para la beca con Id {solicitud.BecaId}." });
            solicitud.Id = _nextId++;
            solicitud.FechaSolicitud = DateTime.Now;
            solicitud.Estado = EstadoSolicitud.Pendiente;
            _solicitudes.Add(solicitud);
            return CreatedAtAction(nameof(GetById), new { id = solicitud.Id }, solicitud);
        }

        [HttpPut("{id}/estado")]
        public IActionResult UpdateEstado(int id, [FromBody] ActualizarEstadoRequest request)
        {
            var solicitud = _solicitudes.FirstOrDefault(s => s.Id == id);
            if (solicitud is null) return NotFound(new { mensaje = $"No se encontró la solicitud con Id {id}." });
            solicitud.Estado = request.NuevoEstado;
            solicitud.Observaciones = request.Observaciones;
            return Ok(solicitud);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var solicitud = _solicitudes.FirstOrDefault(s => s.Id == id);
            if (solicitud is null) return NotFound(new { mensaje = $"No se encontró la solicitud con Id {id}." });
            _solicitudes.Remove(solicitud);
            return NoContent();
        }
    }

    public class ActualizarEstadoRequest
    {
        public EstadoSolicitud NuevoEstado { get; set; }
        public string? Observaciones { get; set; }
    }
}