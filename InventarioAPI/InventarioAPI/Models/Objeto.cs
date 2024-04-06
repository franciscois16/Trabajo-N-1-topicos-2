using System.Security.Cryptography;

namespace InventarioAPI.Models
{
    public class Objeto
    {
        public string obId { get; set; }
        public string? obNombre { get; set; }
        public string? catId { get; set; }
        public int? obCantidad { get; set; }
        public DateTime? obFecCreacion { get; set; }
        public string? obVigencia { get; set; }
    }
}
