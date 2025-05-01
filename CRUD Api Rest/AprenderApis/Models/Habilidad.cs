namespace AprenderApis.Models
{
    public class Habilidad
    {
        public int Id { get; set; }

        public string nombre { get; set; } = string.Empty;

        public EPotencia potencia { get; set; }

        public enum EPotencia
        {
            Suave,
            Moderado,
            Intenso,
            Potente,
            Extremo
        }
    }
}
