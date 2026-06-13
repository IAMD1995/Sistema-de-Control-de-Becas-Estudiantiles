using SistemaControlBecas.Api.Models.Enums;

namespace SistemaControlBecas.Api.Models.Dtos
{
    public class SolicitudDTO
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public int BecaId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public EstadoSolicitud Estado { get; set; }
        public string? Observaciones { get; set; }
    }
}