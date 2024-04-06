namespace InventarioWeb.Models
{
    public class Paquete
    {
        public string paqId { get; set; }
        public string? paqNombre { get; set; }
        public int? paqCantObjetos { get; set; }
        public DateTime? paqFecCreacion { get; set; }
        public List<PaqueteDetalle>? paqD { get; set; }
        public string? obId { get; set; }
        public int? paqDCantObjeto { get; set; }
    }
}