namespace SistemaControlBecas.Api.Models.Dtos
{
    /// <summary>
    /// DTO de entrada para crear una Beca.
    /// </summary>
    public class CreateBecaDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal MontoMensual { get; set; }
        public double PromedioMinimo { get; set; }
        public int CupoDisponible { get; set; }
    }
}