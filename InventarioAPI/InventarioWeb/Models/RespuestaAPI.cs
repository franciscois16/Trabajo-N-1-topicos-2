namespace InventarioWeb.Models
{
    public class RespuestaAPI
    {
        public string mensaje { get; set; }
        public Objeto oObjeto { get; set; }
        public List<Objeto> lObjetos { get; set; }
        public Categoria oCategoria { get; set; }
        public List<Categoria> lCategoria { get; set; }
        public Paquete oPaquete { get; set; }
        public List<Paquete> lPaquetes { get; set; }
        public List<PaqueteDetalle> lPaquetesD { get; set; }
    }
}
