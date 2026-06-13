namespace SistemaControlBecas.Api.Models.Dtos
{
    public class CreateEstudianteDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public double Promedio { get; set; }
    }
}