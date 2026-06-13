namespace SistemaControlBecas.Api.Models.Entities
{
    public class Beca
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal MontoMensual { get; set; }
        public double PromedioMinimo { get; set; }
        public int CupoDisponible { get; set; }
        public bool EstaActiva { get; set; } = true;
    }
}
