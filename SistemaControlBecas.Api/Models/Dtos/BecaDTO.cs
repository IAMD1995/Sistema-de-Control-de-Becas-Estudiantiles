namespace SistemaControlBecas.Api.Models.Dtos
{
    /// <summary>
    /// DTO de salida para Beca.
    /// </summary>
    public class BecaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal MontoMensual { get; set; }
        public double PromedioMinimo { get; set; }
        public int CupoDisponible { get; set; }
        public bool EstaActiva { get; set; }
    }
}