using InventarioWeb.Models;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Net.Http.Headers;
using System.Text;

namespace InventarioWeb.Servicios
{
    public class Servicios : IServicios
    {
        private readonly IHttpClientFactory _clientFactory;
        bool _respuesta = false;
        public Servicios(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<List<Objeto>> ListaObjetos()
        {
            List<Objeto> lObjeto = new List<Objeto>();
            var cliente = _clientFactory.CreateClient("local");
            var response = await cliente.GetAsync("api/objeto/lista");
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<RespuestaAPI>(json_respuesta);
                lObjeto = resultado.lObjetos;
                // lObjeto = JsonConvert.DeserializeObject<List<Objeto>>(json_respuesta); esta sola funciona de 10 si el json que se pasa no es una coleccion y solo es una lista de objetos dentro del json
            }
            return lObjeto;
        }
        public async Task<Objeto> ObtenerObjeto(string obId)
        {
            Objeto oObjeto = new Objeto();
            var cliente = _clientFactory.CreateClient("local");
            var response = await cliente.GetAsync($"api/objeto/obtener?obId={obId}");
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<RespuestaAPI>(json_respuesta);
                oObjeto = resultado.oObjeto;
                // lObjeto = JsonConvert.DeserializeObject<List<Objeto>>(json_respuesta); esta sola funciona de 10 si el json que se pasa no es una coleccion y solo es una lista de objetos dentro del json
            }
            return oObjeto;
        }
        public async Task<bool> CrearObjeto(Objeto oObjeto)
        {
            var cliente = _clientFactory.CreateClient("local");
            var content = new StringContent(JsonConvert.SerializeObject(oObjeto), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync($"api/objeto/crear/", content);
            if (response.IsSuccessStatusCode)
            {
               _respuesta = true;
            }
            return _respuesta;
        }
        public async Task<bool> ModificarObjeto(Objeto oObjeto)
        {
            var cliente = _clientFactory.CreateClient("local");
            var content = new StringContent(JsonConvert.SerializeObject(oObjeto), Encoding.UTF8, "application/json");
            var response = await cliente.PutAsync($"api/objeto/modificar/", content);
            if (response.IsSuccessStatusCode)
            {
                _respuesta = true;
            }
            return _respuesta;
        }
        public async Task<bool> VigenciaObjeto(string obId)
        {
            var cliente = _clientFactory.CreateClient("local");
            var response = await cliente.GetAsync($"api/objeto/vigencia?obId={obId}");
            if (response.IsSuccessStatusCode)
            {
                _respuesta = true;
            }
            return _respuesta;
        }
        public async Task<bool> EliminarObjeto(string obId)
        {
            var cliente = _clientFactory.CreateClient("local");
            var response = await cliente.DeleteAsync($"api/objeto/eliminar?obId={obId}");
            if (response.IsSuccessStatusCode)
            {
                _respuesta = true;
            }
            return _respuesta;
        }
        public async Task<List<Categoria>> ListaCategorias()
        {
            List<Categoria> lCategoria = new List<Categoria>();
            var cliente = _clientFactory.CreateClient("local");
            var response = await cliente.GetAsync("api/categoria/lista");
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<RespuestaAPI>(json_respuesta);
                lCategoria = resultado.lCategoria;
                // lObjeto = JsonConvert.DeserializeObject<List<Objeto>>(json_respuesta); esta sola funciona de 10 si el json que se pasa no es una coleccion y solo es una lista de objetos dentro del json
            }
            return lCategoria;
        }
        public async Task<Categoria> ObtenerCategoria(string catId)
        {
            Categoria oCategoria = new Categoria();
            var cliente = _clientFactory.CreateClient("local");
            var response = await cliente.GetAsync($"api/categoria/obtener?catId={catId}");
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<RespuestaAPI>(json_respuesta);
                oCategoria = resultado.oCategoria;
                // lObjeto = JsonConvert.DeserializeObject<List<Objeto>>(json_respuesta); esta sola funciona de 10 si el json que se pasa no es una coleccion y solo es una lista de objetos dentro del json
            }
            return oCategoria;
        }
        public async Task<bool> CrearCategoria(Categoria oCategoria)
        {
            var cliente = _clientFactory.CreateClient("local");
            var content = new StringContent(JsonConvert.SerializeObject(oCategoria), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync($"api/categoria/crear/", content);
            if (response.IsSuccessStatusCode)
            {
                _respuesta = true;
            }
            return _respuesta;
        }
        public async Task<bool> ModificarCategoria(Categoria oCategoria)
        {
            var cliente = _clientFactory.CreateClient("local");
            var content = new StringContent(JsonConvert.SerializeObject(oCategoria), Encoding.UTF8, "application/json");
            var response = await cliente.PutAsync($"api/categoria/modificar/", content);
            if (response.IsSuccessStatusCode)
            {
                _respuesta = true;
            }
            return _respuesta;
        }
        public async Task<bool> EliminarCategoria(string catId)
        {
            var cliente = _clientFactory.CreateClient("local");
            var response = await cliente.DeleteAsync($"api/categoria/eliminar?catId={catId}");
            if (response.IsSuccessStatusCode)
            {
                _respuesta = true;
            }
            return _respuesta;
        }
        public async Task<List<Paquete>> ListaPaquetes()
        {
            List<Paquete> lPaquete = new List<Paquete>();
            var cliente = _clientFactory.CreateClient("local");
            var response = await cliente.GetAsync("api/paquete/listap");
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<RespuestaAPI>(json_respuesta);
                lPaquete = resultado.lPaquetes;
                // lObjeto = JsonConvert.DeserializeObject<List<Objeto>>(json_respuesta); esta sola funciona de 10 si el json que se pasa no es una coleccion y solo es una lista de objetos dentro del json
            }
            return lPaquete;
        }
        public async Task<List<PaqueteDetalle>> ListaPaqueteDetalle(string paqId)
        {
            List<PaqueteDetalle> lPaqueteD = new List<PaqueteDetalle>();
            var cliente = _clientFactory.CreateClient("local");
            var response = await cliente.GetAsync($"api/paquete/listad?paqId={paqId}");
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<RespuestaAPI>(json_respuesta);
                lPaqueteD = resultado.lPaquetesD;
                // lObjeto = JsonConvert.DeserializeObject<List<Objeto>>(json_respuesta); esta sola funciona de 10 si el json que se pasa no es una coleccion y solo es una lista de objetos dentro del json
            }
            return lPaqueteD;
        }
        public async Task<Paquete> ObtenerPaquete(string paqId)
        {
            Paquete oPaquete = new Paquete();
            var cliente = _clientFactory.CreateClient("local");
            var response = await cliente.GetAsync($"api/paquete/obtener?paqId={paqId}");
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<RespuestaAPI>(json_respuesta);
                oPaquete = resultado.oPaquete;
                // lObjeto = JsonConvert.DeserializeObject<List<Objeto>>(json_respuesta); esta sola funciona de 10 si el json que se pasa no es una coleccion y solo es una lista de objetos dentro del json
            }
            return oPaquete;
        }
        public async Task<bool> CrearPaquete(Paquete oPaquete)
        {
            var cliente = _clientFactory.CreateClient("local");
            var content = new StringContent(JsonConvert.SerializeObject(oPaquete), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync($"api/paquete/crear/", content);
            if (response.IsSuccessStatusCode)
            {
                _respuesta = true;
            }
            return _respuesta;
        }
        public async Task<bool> EliminarPaquete(string paqId)
        {
            var cliente = _clientFactory.CreateClient("local");
            var response = await cliente.DeleteAsync($"api/paquete/eliminar?paqId= {paqId}");
            if (response.IsSuccessStatusCode)
            {
                _respuesta = true;
            }
            return _respuesta;
        }
    }
}
