using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuEscuela.Models;
using TuEscuela.ModelView;

namespace TuEscuela.Controllers
{
    public class AdministracionController : Controller
    {
        //
        // GET: /Administracion/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Carreras()
        {
            List<Carrera> carreras = new List<Carrera>();
            try
            {
                DataTable dtCarreras = new DataTable();
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["EscuelaConnectionString"].ConnectionString))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("spGetCarreras", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        SqlDataReader dr = comando.ExecuteReader();
                        dtCarreras.Load(dr);
                    }
                }

                foreach (DataRow row in dtCarreras.Rows)
                {
                    carreras.Add(new Carrera() { IdCarrera = Convert.ToInt32(row["IdCarrera"]), NombreCarrera = row["NombreCarrera"].ToString() });
                }

            }
            catch (Exception ex)
            {
                //WriteLog(ex);
            }

            return View(carreras);
        }

        public ActionResult DetallesCarrera(int id)
        {
            PlanEstudios planEstudios = new PlanEstudios();
            DataSet dsPlanEstudios = new DataSet();

            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["EscuelaConnectionString"].ConnectionString))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("spGetPlanEstudios", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdCarrera", id);
                    SqlDataAdapter DA = new SqlDataAdapter(comando);
                    DA.Fill(dsPlanEstudios);
                }
            }

            if (dsPlanEstudios.Tables.Count > 0)
            {
                planEstudios.IdCarrera = id;
                planEstudios.NombreCarrera = dsPlanEstudios.Tables[0].Rows[0]["NombreCarrera"].ToString();

                List<DetallePlanEstudios> detallesPlanEstudios = new List<DetallePlanEstudios>();
                
                foreach (DataRow row in dsPlanEstudios.Tables[1].Rows)
                {
                    detallesPlanEstudios.Add(new DetallePlanEstudios()
                        {
                            IdCarrera = Convert.ToInt32(row["IdCarrera"]),
                            NombreCarrera = row["NombreCarrera"].ToString(),
                            Creditos = Convert.ToInt32(row["Creditos"]),
                            IdMateria = Convert.ToInt32(row["IdMateria"]), 
                            Nivel = Convert.ToInt32(row["Nivel"]), 
                            NombreMateria = row["NombreMateria"].ToString(),
                            TipoNivel = row["TipoNivel"].ToString(),
                            Niveles = Convert.ToInt32(row["Niveles"])
                        }
                    );
                }

                var Niveles = (from d in detallesPlanEstudios
                        group d by new { d.Nivel, d.TipoNivel }
                            into gcs
                            select new NivelCarrera() { Nivel = gcs.Key.Nivel, TipoNivel = gcs.Key.TipoNivel, Materias = new List<Materia>() }).ToList();


                foreach (var nivel in Niveles)
                {
                    var detalles = (from m in detallesPlanEstudios where m.Nivel == nivel.Nivel && m.TipoNivel == nivel.TipoNivel select m);
                    
                    foreach(var detalle in detalles)
                    {
                        nivel.Materias.Add(new Materia() { IdMateria = detalle.IdMateria, NombreMateria = detalle.NombreMateria, Creditos = detalle.Creditos });
                    }
                }

                planEstudios.Niveles = Niveles;

            }

            return View(planEstudios);
        }
	}
}