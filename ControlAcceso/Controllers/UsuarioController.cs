using ControlAcceso.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ControlAcceso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly string _conexionSQL;
        public UsuarioController(IConfiguration config)
        {
            _conexionSQL = config.GetConnectionString("ConexionBDSQL");
        }
        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Usuario> lUsuarios= new List<Usuario>();
            try
            {
                using (SqlConnection connSQL = new SqlConnection(_conexionSQL))
                {
                    connSQL.Open();
                    SqlCommand cmd = new SqlCommand("Mantenedor_Usuario", connSQL);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@modo", "L");
                    using (SqlDataReader resultadoSP = cmd.ExecuteReader())
                    {
                        while (resultadoSP.Read())
                        {
                            lUsuarios.Add(new Usuario()
                            {
                                usuId = resultadoSP[0].ToString(),
                                usuNombre = resultadoSP[1].ToString(),
                            }); ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", lUsuarios = lUsuarios });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, lUsuarios = lUsuarios });
            }
        }
        [HttpPost]
        [Route("Crear")]
        public IActionResult Crear([FromBody] Usuario nuevoUsuario)
        {
            try
            {
                using (SqlConnection connSQL = new SqlConnection(_conexionSQL))
                {
                    connSQL.Open();
                    SqlCommand cmd = new SqlCommand("Mantenedor_Usuario", connSQL);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuId", nuevoUsuario.usuId);
                    cmd.Parameters.AddWithValue("@usuNombre", nuevoUsuario.usuNombre);
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
    }
}
