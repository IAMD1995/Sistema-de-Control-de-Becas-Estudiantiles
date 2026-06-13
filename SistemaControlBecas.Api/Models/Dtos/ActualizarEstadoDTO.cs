using SistemaControlBecas.Api.Models.Enums;

namespace SistemaControlBecas.Api.Models.Dtos
{
    public class ActualizarEstadoDTO
    {
        public EstadoSolicitud NuevoEstado { get; set; }
        public string? Observaciones { get; set; }
    }
}