using System.Security.Cryptography;

namespace InventarioAPI.Models
{
    public class PaqueteDetalle
    {
        public int? paqDId { get; set; }
        public string? obId { get; set; }
        public int? paqDCantObjeto { get; set; }
    }
}
