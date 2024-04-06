using InventarioAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace InventarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly string _conexionSQL;
        public CategoriaController(IConfiguration config)
        {
            _conexionSQL = config.GetConnectionString("ConexionBDSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Categoria> listaCategoria = new List<Categoria>();
            try
            {
                using (SqlConnection connSQL = new SqlConnection(_conexionSQL))
                {
                    connSQL.Open();
                    SqlCommand cmd = new SqlCommand("MantenedorCategorias", connSQL);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@modo", "L");
                    using (SqlDataReader resultadoSP = cmd.ExecuteReader())
                    {
                        while (resultadoSP.Read())
                        {
                            listaCategoria.Add(new Categoria()
                            {
                                catId = resultadoSP[0].ToString(),
                                catNombre = resultadoSP[1].ToString(),
                            }); ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", lCategoria = listaCategoria });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, lCategoria = listaCategoria });
            }
        }
        [HttpGet]
        [Route("Obtener")]
        public IActionResult Obtener(string catId)
        {
            Categoria datosCategoria = new Categoria();
            try
            {
                using (SqlConnection connSQL = new SqlConnection(_conexionSQL))
                {
                    connSQL.Open();
                    SqlCommand cmd = new SqlCommand("MantenedorCategorias", connSQL);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@catId", catId);
                    cmd.Parameters.AddWithValue("@modo", "O");
                    using (SqlDataReader resultadoSP = cmd.ExecuteReader())
                    {
                        while (resultadoSP.Read())
                        {
                            datosCategoria = new Categoria()
                            {
                                catId = resultadoSP[0].ToString(),
                                catNombre = resultadoSP[1].ToString(),
                            };
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", oCategoria = datosCategoria });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, oCategoria = datosCategoria });
            }
        }
        [HttpPost]
        [Route("Crear")]
        public IActionResult Crear([FromBody] Categoria nuevaCategoria)
        {
            try
            {
                using (SqlConnection connSQL = new SqlConnection(_conexionSQL))
                {
                    connSQL.Open();
                    SqlCommand cmd = new SqlCommand("MantenedorCategorias", connSQL);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@catId", nuevaCategoria.catId);
                    cmd.Parameters.AddWithValue("@catNombre", nuevaCategoria.catNombre);
                    cmd.Parameters.AddWithValue("@modo", "C");
                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }

        }
        [HttpPut]
        [Route("Modificar")]
        public IActionResult Modificar([FromBody] Categoria categoria)
        {
            try
            {
                using (SqlConnection connSQL = new SqlConnection(_conexionSQL))
                {
                    connSQL.Open();
                    SqlCommand cmd = new SqlCommand("MantenedorCategorias", connSQL);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@catId", categoria.catId);
                    cmd.Parameters.AddWithValue("@catNombre", categoria.catNombre);
                    cmd.Parameters.AddWithValue("@modo", "M");
                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        [HttpDelete]
        [Route("Eliminar")]
        public IActionResult Eliminar(string catId)
        {
            try
            {
                using (SqlConnection connSQL = new SqlConnection(_conexionSQL))
                {
                    connSQL.Open();
                    SqlCommand cmd = new SqlCommand("MantenedorCategoria", connSQL);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuID", catId);
                    cmd.Parameters.AddWithValue("@modo", "E");
                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
    }
}
