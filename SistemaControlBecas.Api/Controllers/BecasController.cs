using Microsoft.AspNetCore.Mvc;
using SistemaControlBecas.Api.Models.Entities;

namespace SistemaControlBecas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BecasController : ControllerBase
    {
        private static List<Beca> _becas = new();
        private static int _nextId = 1;

        [HttpGet]
        public IActionResult GetAll() => Ok(_becas);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var beca = _becas.FirstOrDefault(b => b.Id == id);
            if (beca is null) return NotFound(new { mensaje = $"No se encontró la beca con Id {id}." });
            return Ok(beca);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Beca beca)
        {
            if (beca.PromedioMinimo < 0 || beca.PromedioMinimo > 4)
                return BadRequest(new { mensaje = "El promedio mínimo debe estar entre 0.0 y 4.0." });
            if (beca.CupoDisponible < 0)
                return BadRequest(new { mensaje = "El cupo disponible no puede ser negativo." });
            beca.Id = _nextId++;
            _becas.Add(beca);
            return CreatedAtAction(nameof(GetById), new { id = beca.Id }, beca);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Beca actualizada)
        {
            var beca = _becas.FirstOrDefault(b => b.Id == id);
            if (beca is null) return NotFound(new { mensaje = $"No se encontró la beca con Id {id}." });
            if (actualizada.PromedioMinimo < 0 || actualizada.PromedioMinimo > 4)
                return BadRequest(new { mensaje = "El promedio mínimo debe estar entre 0.0 y 4.0." });
            if (actualizada.CupoDisponible < 0)
                return BadRequest(new { mensaje = "El cupo disponible no puede ser negativo." });
            beca.Nombre = actualizada.Nombre;
            beca.Descripcion = actualizada.Descripcion;
            beca.MontoMensual = actualizada.MontoMensual;
            beca.PromedioMinimo = actualizada.PromedioMinimo;
            beca.CupoDisponible = actualizada.CupoDisponible;
            beca.EstaActiva = actualizada.EstaActiva;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var beca = _becas.FirstOrDefault(b => b.Id == id);
            if (beca is null) return NotFound(new { mensaje = $"No se encontró la beca con Id {id}." });
            _becas.Remove(beca);
            return NoContent();
        }
    }
}