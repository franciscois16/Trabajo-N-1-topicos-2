using InventarioWeb.Models;
using System;

namespace InventarioWeb.Servicios
{
    public interface IServicios
    {
        Task<List<Objeto>> ListaObjetos();
        Task<Objeto> ObtenerObjeto(string obId);
        Task<bool> CrearObjeto(Objeto oObjeto);
        Task<bool> ModificarObjeto(Objeto oObjeto);
        Task<bool> VigenciaObjeto(string obId);
        Task<bool> EliminarObjeto(string obId);
        Task<List<Categoria>> ListaCategorias();
        Task<Categoria> ObtenerCategoria(string catId);
        Task<bool> CrearCategoria(Categoria oCategoria);
        Task<bool> ModificarCategoria(Categoria oCategoria);
        Task<bool> EliminarCategoria(string catId);
        Task<List<Paquete>> ListaPaquetes();
        Task<List<PaqueteDetalle>> ListaPaqueteDetalle(string paqId);
        Task<Paquete> ObtenerPaquete(string paqId);
        Task<bool> CrearPaquete(Paquete oPaquete);
        Task<bool> EliminarPaquete(string paqId);
    }
}
