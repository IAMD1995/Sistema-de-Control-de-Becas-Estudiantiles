namespace SistemaControlBecas.Api.Models.Dtos
{
    public class CreateSolicitudDTO
    {
        public int EstudianteId { get; set; }
        public int BecaId { get; set; }
        public string? Observaciones { get; set; }
    }
}