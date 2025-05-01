namespace AprenderApis.Models
{
    public class Mandril
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public List<Habilidad> Habilidades { get; set; } = new List<Habilidad>();

    }
}
