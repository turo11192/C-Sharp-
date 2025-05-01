using System.ComponentModel.DataAnnotations;

namespace AprenderApis.Models
{
    public class MandrilInsert
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;
    }
}
