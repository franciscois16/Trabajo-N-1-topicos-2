using InventarioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace InventarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjetoController : ControllerBase
    {
        private readonly string _conexionSQL;
        public ObjetoController(IConfiguration config)
        {
            _conexionSQL = config.GetConnectionString("ConexionBDSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Objeto> listaObjetos = new List<Objeto>();
            try
            {
                using (SqlConnection connSQL = new SqlConnection(_conexionSQL))
                {
                    connSQL.Open();
                    SqlCommand cmd = new SqlCommand("MantenedorObjetos", connSQL);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@modo", "L");
                    using (SqlDataReader resultadoSP = cmd.ExecuteReader())
                    {
                        while (resultadoSP.Read())
                        {
                            listaObjetos.Add(new Objeto()
                            {
                                obId = resultadoSP[0].ToString(),
                                obNombre = resultadoSP[1].ToString(),
                                catId= resultadoSP[2].ToString(),
                                obCantidad = Convert.ToInt32(resultadoSP[3]),
                                obFecCreacion = resultadoSP.GetDateTime(4),
                                obVigencia = resultadoSP[5].ToString(),
                            }); 
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", lObjetos = listaObjetos });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, lObjetos = listaObjetos });
            }
        }
        [HttpGet]
        [Route("Obtener")]
        public IActionResult Obtener(string obId)
        {
            Objeto datosObjeto = new Objeto();
            try
            {
                using (SqlConnection connSQL = new SqlConnection(_conexionSQL))
                {
                    connSQL.Open();
                    SqlCommand cmd = new SqlCommand("MantenedorObjetos", connSQL);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@obId", obId);
                    cmd.Parameters.AddWithValue("@modo", "O");
                    using (SqlDataReader resultadoSP = cmd.ExecuteReader())
                    {
                        while (resultadoSP.Read())
                        {
                            datosObjeto = new Objeto()
                            {
                                obId = resultadoSP[0].ToString(),
                                obNombre = resultadoSP[1].ToString(),
                                catId = resultadoSP[2].ToString(),
                                obCantidad = Convert.ToInt32(resultadoSP[3]),
                                obFecCreacion = resultadoSP.GetDateTime(4),
                                obVigencia = resultadoSP[5].ToString(),
                            };
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", oObjeto = datosObjeto });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, oObjeto = datosObjeto });
            }
        }
        [HttpPost]
        [Route("Crear")]
        public IActionResult Crear([FromBody] Objeto nuevoObjeto)
        {
            try
            {
                using (SqlConnection connSQL = new SqlConnection(_conexionSQL))
                {
                    connSQL.Open();
                    SqlCommand cmd = new SqlCommand("MantenedorObjetos", connSQL);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@obId", nuevoObjeto.obId);
                    cmd.Parameters.AddWithValue("@obNombre", nuevoObjeto.obNombre);
                    cmd.Parameters.AddWithValue("@catId", nuevoObjeto.catId);
                    cmd.Parameters.AddWithValue("@obCantidad", nuevoObjeto.obCantidad);
                    cmd.Parameters.AddWithValue("@obFecCreacion", nuevoObjeto.obFecCreacion);
                    cmd.Parameters.AddWithValue("@obVigencia", nuevoObjeto.obVigencia);
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
        public IActionResult Modificar([FromBody] Objeto objeto)
        {
            try
            {
                using (SqlConnection connSQL = new SqlConnection(_conexionSQL))
                {
                    connSQL.Open();
                    SqlCommand cmd = new SqlCommand("MantenedorObjetos", connSQL);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@obId", objeto.obId);
                    cmd.Parameters.AddWithValue("@obNombre", objeto.obNombre);
                    cmd.Parameters.AddWithValue("@catId", objeto.catId);
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
        [HttpGet]
        [Route("Vigencia")]
        public IActionResult Vigencia(string obId)
        {
            try
            {
                using (SqlConnection connSQL = new SqlConnection(_conexionSQL))
                {
                    connSQL.Open();
                    SqlCommand cmd = new SqlCommand("MantenedorObjetos", connSQL);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@obId", obId);
                    cmd.Parameters.AddWithValue("@modo", "V");
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
        public IActionResult Eliminar(string obId)
        {
            try
            {
                using (SqlConnection connSQL = new SqlConnection(_conexionSQL))
                {
                    connSQL.Open();
                    SqlCommand cmd = new SqlCommand("MantenedorCategoria", connSQL);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@obId", obId);
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
