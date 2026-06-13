using Microsoft.AspNetCore.Mvc;
using SistemaControlBecas.Api.Data;
using SistemaControlBecas.Api.Models.Dtos;
using SistemaControlBecas.Api.Models.Entities;

namespace SistemaControlBecas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BecasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BecasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/becas
        [HttpGet]
        public ActionResult<List<BecaDTO>> GetAll()
        {
            var becas = _context.Becas.ToList();

            var becaDTOs = becas.Select(b => new BecaDTO
            {
                Id = b.Id,
                Nombre = b.Nombre,
                Descripcion = b.Descripcion,
                MontoMensual = b.MontoMensual,
                PromedioMinimo = b.PromedioMinimo,
                CupoDisponible = b.CupoDisponible,
                EstaActiva = b.EstaActiva
            }).ToList();

            return Ok(becaDTOs);
        }

        // GET: api/becas/{id}
        [HttpGet("{id}")]
        public ActionResult<BecaDTO> GetById(int id)
        {
            var beca = _context.Becas.Find(id);
            if (beca == null)
                return NotFound(new { mensaje = $"No se encontró la beca con Id {id}." });

            var becaDTO = new BecaDTO
            {
                Id = beca.Id,
                Nombre = beca.Nombre,
                Descripcion = beca.Descripcion,
                MontoMensual = beca.MontoMensual,
                PromedioMinimo = beca.PromedioMinimo,
                CupoDisponible = beca.CupoDisponible,
                EstaActiva = beca.EstaActiva
            };

            return Ok(becaDTO);
        }

        // POST: api/becas
        [HttpPost]
        public ActionResult<BecaDTO> Create([FromBody] CreateBecaDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre))
                return BadRequest(new { mensaje = "El nombre de la beca es obligatorio." });

            if (request.PromedioMinimo < 0 || request.PromedioMinimo > 4)
                return BadRequest(new { mensaje = "El promedio mínimo debe estar entre 0.0 y 4.0." });

            if (request.CupoDisponible < 0)
                return BadRequest(new { mensaje = "El cupo disponible no puede ser negativo." });

            var newBeca = new Beca
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                MontoMensual = request.MontoMensual,
                PromedioMinimo = request.PromedioMinimo,
                CupoDisponible = request.CupoDisponible,
                EstaActiva = true
            };

            _context.Becas.Add(newBeca);
            _context.SaveChanges();

            var becaDTO = new BecaDTO
            {
                Id = newBeca.Id,
                Nombre = newBeca.Nombre,
                Descripcion = newBeca.Descripcion,
                MontoMensual = newBeca.MontoMensual,
                PromedioMinimo = newBeca.PromedioMinimo,
                CupoDisponible = newBeca.CupoDisponible,
                EstaActiva = newBeca.EstaActiva
            };

            return CreatedAtAction(nameof(GetById), new { id = newBeca.Id }, becaDTO);
        }

        // PUT: api/becas/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CreateBecaDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre))
                return BadRequest(new { mensaje = "El nombre de la beca es obligatorio." });

            if (request.PromedioMinimo < 0 || request.PromedioMinimo > 4)
                return BadRequest(new { mensaje = "El promedio mínimo debe estar entre 0.0 y 4.0." });

            if (request.CupoDisponible < 0)
                return BadRequest(new { mensaje = "El cupo disponible no puede ser negativo." });

            var existingBeca = _context.Becas.Find(id);
            if (existingBeca == null)
                return NotFound(new { mensaje = $"No se encontró la beca con Id {id}." });

            existingBeca.Nombre = request.Nombre;
            existingBeca.Descripcion = request.Descripcion;
            existingBeca.MontoMensual = request.MontoMensual;
            existingBeca.PromedioMinimo = request.PromedioMinimo;
            existingBeca.CupoDisponible = request.CupoDisponible;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/becas/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var beca = _context.Becas.Find(id);
            if (beca == null)
                return NotFound(new { mensaje = $"No se encontró la beca con Id {id}." });

            bool tieneSolicitudes = _context.Solicitudes.Any(s => s.BecaId == id);
            if (tieneSolicitudes)
                return BadRequest(new { mensaje = "No se puede eliminar la beca porque tiene solicitudes asociadas." });

            _context.Becas.Remove(beca);
            _context.SaveChanges();

            return NoContent();
        }
    }
}