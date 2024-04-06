using InventarioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Serialization;
using System.Text;

namespace InventarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaqueteController : ControllerBase
    {
        private readonly string _conexionSQL;
        public PaqueteController(IConfiguration config)
        {
            _conexionSQL = config.GetConnectionString("ConexionBDSQL");
        }

        [HttpGet]
        [Route("ListaP")]
        public IActionResult ListaPaquetes()
        {
            List<Paquete> listaPaquetes = new List<Paquete>();
            try
            {
                using (SqlConnection connSQL = new SqlConnection(_conexionSQL))
                {
                    connSQL.Open();
                    SqlCommand cmd = new SqlCommand("MantenedorPaquetes", connSQL);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@modo", "P");
                    using (SqlDataReader resultadoSP = cmd.ExecuteReader())
                    {
                        while (resultadoSP.Read())
                        {
                            listaPaquetes.Add(new Paquete()
                            {
                                paqId = resultadoSP[0].ToString(),
                                paqNombre = resultadoSP[1].ToString(),
                                paqCantObjetos = Convert.ToInt32(resultadoSP[2]),
                                paqFecCreacion = resultadoSP.GetDateTime(3),
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", lPaquetes = listaPaquetes });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, lPaquetes = listaPaquetes });
            }
        }
        [HttpGet]
        [Route("ListaD")]
        public IActionResult ListaDetalle(string paqId)
        {
            List<PaqueteDetalle> listaPaquetesD = new List<PaqueteDetalle>();
            try
            {
                using (SqlConnection connSQL = new SqlConnection(_conexionSQL))
                {
                    connSQL.Open();
                    SqlCommand cmd = new SqlCommand("MantenedorPaquetes", connSQL);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@paqId", paqId);
                    cmd.Parameters.AddWithValue("@modo", "D");
                    using (SqlDataReader resultadoSP = cmd.ExecuteReader())
                    {
                        while (resultadoSP.Read())
                        {
                            listaPaquetesD.Add(new PaqueteDetalle()
                            {
                                paqDId = Convert.ToInt32(resultadoSP[0]),
                                obId = resultadoSP[1].ToString(),
                                paqDCantObjeto = Convert.ToInt32(resultadoSP[2]),
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", lPaquetesD = listaPaquetesD });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, lPaquetesD = listaPaquetesD });
            }
        }
        [HttpGet]
        [Route("Obtener")]
        public IActionResult ObtenerPaquete(string paqId)
        {
            Paquete oPaquete = new Paquete();
            try
            {
                using (SqlConnection connSQL = new SqlConnection(_conexionSQL))
                {
                    connSQL.Open();
                    SqlCommand cmd = new SqlCommand("MantenedorObjetos", connSQL);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@paqId", paqId);
                    cmd.Parameters.AddWithValue("@modo", "O");
                    using (SqlDataReader resultadoSP = cmd.ExecuteReader())
                    {
                        while (resultadoSP.Read())
                        {
                            oPaquete = new Paquete()
                            {
                                paqId = paqId,
                                paqNombre = resultadoSP[0].ToString(),
                                paqCantObjetos = Convert.ToInt32(resultadoSP[1]),
                                paqFecCreacion = resultadoSP.GetDateTime(2),
                            };
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", oPaquete = oPaquete });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, oPaquete = oPaquete });
            }
        }
        [HttpPost]
        [Route("Crear")]
        public IActionResult Crear([FromBody] Paquete nuevoPaquete)
        {
            try
            {
                using (SqlConnection connSQL = new SqlConnection(_conexionSQL))
                {
                    string xml = null;
                    using (StringWriter sw = new Utf8StringWriter())
                    {

                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        ns.Add("", "");
                        XmlSerializer xs = new XmlSerializer(typeof(List<PaqueteDetalle>));
                        xs.Serialize(sw, nuevoPaquete.paqD, ns);
                        xml = sw.ToString();
                        connSQL.Open();
                        SqlCommand cmd = new SqlCommand("MantenedorPaquetes", connSQL);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@paqId", nuevoPaquete.paqId);
                        cmd.Parameters.AddWithValue("@paqNombre", nuevoPaquete.paqNombre);
                        cmd.Parameters.AddWithValue("@paqCantObjetos", nuevoPaquete.paqCantObjetos);
                        cmd.Parameters.AddWithValue("@paqFecCreacion", nuevoPaquete.paqFecCreacion);
                        cmd.Parameters.AddWithValue("@xml", xml);
                        cmd.Parameters.AddWithValue("@modo", "C");
                        cmd.ExecuteNonQuery();
                    }
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
        public IActionResult Eliminar(string paqId)
        {
            try
            {
                using (SqlConnection connSQL = new SqlConnection(_conexionSQL))
                {
                    connSQL.Open();
                    SqlCommand cmd = new SqlCommand("MantenedorPaquetes", connSQL);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@paqId", paqId);
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
        public class Utf8StringWriter : StringWriter { public override Encoding Encoding { get { return Encoding.UTF8; } } }
    }
}
