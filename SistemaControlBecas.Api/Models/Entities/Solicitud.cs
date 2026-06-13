using SistemaControlBecas.Api.Models.Enums;

namespace SistemaControlBecas.Api.Models.Entities
{
    public class Solicitud
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public int BecaId { get; set; }
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;
        public EstadoSolicitud Estado { get; set; } = EstadoSolicitud.Pendiente;
        public string? Observaciones { get; set; }
    }
}
