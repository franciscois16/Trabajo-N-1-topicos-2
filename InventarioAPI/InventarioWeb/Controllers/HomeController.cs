using InventarioWeb.Models;
using InventarioWeb.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InventarioWeb.Controllers
{
    public class HomeController : Controller
    {

        private readonly IServicios _serviciosApi;
        public HomeController(IServicios serviciosApi)
        {
            _serviciosApi = serviciosApi;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> MantenedorObjetos()
        {
            List<Objeto> lObjeto = await _serviciosApi.ListaObjetos();
            return View(lObjeto);
        }
        public async Task<IActionResult> Objeto()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CMObjeto(Objeto objeto)
        {
            bool respuesta = false;
            respuesta = await _serviciosApi.CrearObjeto(objeto);
            if (respuesta)
            {
                return RedirectToAction("MantenedorObjetos");
            }
            else
            {
                return View("Objeto", objeto);
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}