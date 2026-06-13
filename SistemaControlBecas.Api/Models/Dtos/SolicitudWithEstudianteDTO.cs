namespace SistemaControlBecas.Api.Models.Dtos
{
    public class SolicitudWithEstudianteDTO
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public string EstudianteNombre { get; set; } = string.Empty;
        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
    }
}