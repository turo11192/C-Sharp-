using static AprenderApis.Models.Habilidad;

namespace AprenderApis.Models
{
    public class HabilidadInsert
    {
        public string nombre { get; set; } = string.Empty;

        public EPotencia potencia { get; set; }
    }
}
