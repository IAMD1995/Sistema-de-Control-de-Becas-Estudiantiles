namespace SistemaControlBecas.Api.Models.Entities
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public double Promedio { get; set; }
        public bool EstaActivo { get; set; } = true;
    }
}
