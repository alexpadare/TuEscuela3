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
    public class AlumnosController : Controller
    {
        //
        // GET: /Alumnos/
        public ActionResult Index()
        {
            List<Alumno> alumnos = new List<Alumno>();
            try
            {
                DataTable dtAlumnos = new DataTable();
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["EscuelaConnectionString"].ConnectionString))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("spGetAlumnos", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        SqlDataReader dr = comando.ExecuteReader();
                        dtAlumnos.Load(dr);
                    }
                }

                foreach (DataRow row in dtAlumnos.Rows)
                {
                    alumnos.Add(new Alumno() { 
                        IdAlumno = Convert.ToInt32(row["IdAlumno"]),
                        Nombre = row["Nombre"].ToString(),
                        ApellidoPaterno= row["ApellidoPat"].ToString(),
                        ApellidoMaterno = row["ApellidoMat"].ToString(),
                        FechaNacimiento = row["FechaNacimiento"] != DBNull.Value ? Convert.ToDateTime(row["FechaNacimiento"]) : new DateTime(1, 1, 1),
                        Tel1 = row["Tel1"] != DBNull.Value ? row["Tel1"].ToString() : "N/A",
                        Genero = row["Genero"] != DBNull.Value ? row["NumControl"].ToString() : "N/A",
                        NumControl = row["NumControl"] != DBNull.Value ? row["NumControl"].ToString() : "N/A"
                    });
                }

            }
            catch (Exception ex)
            {
                //WriteLog(ex);
            }

            return View(alumnos);
        }

        public ActionResult CrearAlumno()
        {
            AlumnoDireccion alumnoDir = new AlumnoDireccion();
            alumnoDir.Alumno = new Alumno();
            alumnoDir.Contactos = new List<Contacto>();
            alumnoDir.Contactos.Add(new Contacto());
            alumnoDir.Contactos.Add(new Contacto());

            return View(alumnoDir);
        }

        public ActionResult Create()
        {
            AlumnoDireccion alumnoDir = new AlumnoDireccion();
            //ViewBag.IdAlumno = new SelectList(db.Calificaciones, "IdAlumno", "IdAlumno");
            return View(alumnoDir);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlumnoDireccion alumnoDir, HttpPostedFileBase imagenAlumno)
        {
            if (ModelState.IsValid)
            {
                
            }
            return View();
        }

        public FileContentResult GetFile(int id)
        {
            DataTable dtImagen = new DataTable();
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["EscuelaConnectionString"].ConnectionString))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("spGetImagenAlumno", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdAlumno", id);
                    SqlDataAdapter DA = new SqlDataAdapter(comando);
                    DA.Fill(dtImagen);
                }
            }

            byte[] alumnoImagen = dtImagen.Rows[0]["Imagen"] != DBNull.Value ? (byte[])dtImagen.Rows[0]["Imagen"] : null;
            return alumnoImagen != null ? new FileContentResult(alumnoImagen, "image/jpeg") : null;
        }

        public ActionResult DetallesAlumno(int id)
        {
            AlumnoDireccion alumnoDireccion = new AlumnoDireccion();
            alumnoDireccion.Alumno = new Alumno();

            DataSet dsAlumnoDireccion = new DataSet();

            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["EscuelaConnectionString"].ConnectionString))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("spGetAlumno", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdAlumno", id);
                    SqlDataAdapter DA = new SqlDataAdapter(comando);
                    DA.Fill(dsAlumnoDireccion);
                }
            }

            if (dsAlumnoDireccion.Tables.Count > 0)
            {
                alumnoDireccion.Alumno.IdAlumno = id;
                alumnoDireccion.Alumno.ApellidoMaterno = dsAlumnoDireccion.Tables[0].Rows[0]["ApellidoMat"].ToString();
                alumnoDireccion.Alumno.ApellidoPaterno = dsAlumnoDireccion.Tables[0].Rows[0]["ApellidoPat"].ToString();
                alumnoDireccion.Alumno.Correo = dsAlumnoDireccion.Tables[0].Rows[0]["Correo"].ToString();
                alumnoDireccion.Alumno.CURP = dsAlumnoDireccion.Tables[0].Rows[0]["CURP"].ToString();
                alumnoDireccion.Alumno.FechaNacimiento = dsAlumnoDireccion.Tables[0].Rows[0]["FechaNacimiento"] != DBNull.Value ? Convert.ToDateTime(dsAlumnoDireccion.Tables[0].Rows[0]["FechaNacimiento"]) : new DateTime(1, 1, 1);
                alumnoDireccion.Alumno.FechaRegistro = Convert.ToDateTime(dsAlumnoDireccion.Tables[0].Rows[0]["FechaRegistro"]);
                alumnoDireccion.Alumno.Genero = dsAlumnoDireccion.Tables[0].Rows[0]["Genero"].ToString();
                alumnoDireccion.Alumno.Nombre = dsAlumnoDireccion.Tables[0].Rows[0]["Nombre"].ToString();
                alumnoDireccion.Alumno.NSS = dsAlumnoDireccion.Tables[0].Rows[0]["NSS"].ToString();
                alumnoDireccion.Alumno.NumControl = dsAlumnoDireccion.Tables[0].Rows[0]["NumControl"] != DBNull.Value ? dsAlumnoDireccion.Tables[0].Rows[0]["NumControl"].ToString() : "";
                alumnoDireccion.Alumno.RFC = dsAlumnoDireccion.Tables[0].Rows[0]["RFC"].ToString();
                alumnoDireccion.Alumno.Tel1 = dsAlumnoDireccion.Tables[0].Rows[0]["Tel1"].ToString();
                alumnoDireccion.Alumno.Tel2 = dsAlumnoDireccion.Tables[0].Rows[0]["Tel2"].ToString();

                alumnoDireccion.Calle = dsAlumnoDireccion.Tables[0].Rows[0]["Calle"].ToString();
                alumnoDireccion.Ciudad = dsAlumnoDireccion.Tables[0].Rows[0]["Ciudad"].ToString();
                alumnoDireccion.Colonia = dsAlumnoDireccion.Tables[0].Rows[0]["Colonia"].ToString();
                alumnoDireccion.CodigoPostal = dsAlumnoDireccion.Tables[0].Rows[0]["CodigoPostal"].ToString();
                alumnoDireccion.Estado = dsAlumnoDireccion.Tables[0].Rows[0]["Estado"].ToString();
                alumnoDireccion.IdDireccion = Convert.ToInt32(dsAlumnoDireccion.Tables[0].Rows[0]["IdDireccion"]);
                alumnoDireccion.IdEstado = Convert.ToInt32(dsAlumnoDireccion.Tables[0].Rows[0]["IdEstado"]);
                alumnoDireccion.IdMunicipio = Convert.ToInt32(dsAlumnoDireccion.Tables[0].Rows[0]["IdMunicipio"]);
                alumnoDireccion.Municipio = dsAlumnoDireccion.Tables[0].Rows[0]["Municipio"].ToString();
                alumnoDireccion.NoExt = dsAlumnoDireccion.Tables[0].Rows[0]["NoExt"].ToString();
                alumnoDireccion.NoInt = dsAlumnoDireccion.Tables[0].Rows[0]["NoInt"].ToString();


                List<Contacto> contactosAlumno = new List<Contacto>();
                foreach (DataRow row in dsAlumnoDireccion.Tables[1].Rows)
                {
                    contactosAlumno.Add(new Contacto() { 
                        ApellidoMaterno = dsAlumnoDireccion.Tables[1].Rows[0]["ApellidoMat"].ToString(),
                        ApellidoPaterno = dsAlumnoDireccion.Tables[1].Rows[0]["ApellidoPat"].ToString(),
                        Correo = dsAlumnoDireccion.Tables[1].Rows[0]["Correo"].ToString(),
                        Descripcion = dsAlumnoDireccion.Tables[1].Rows[0]["Descripcion"].ToString(),
                        IdContacto = Convert.ToInt32(dsAlumnoDireccion.Tables[1].Rows[0]["IdContacto"]),
                        IdTipoRelacion = Convert.ToInt32(dsAlumnoDireccion.Tables[1].Rows[0]["IdTipoRelacion"]),
                        Nombre = dsAlumnoDireccion.Tables[1].Rows[0]["Nombre"].ToString(),
                        Telefono = dsAlumnoDireccion.Tables[1].Rows[0]["Telefono"].ToString(),

                    });
                    
                }

                alumnoDireccion.Contactos = contactosAlumno;

            }

            return View(alumnoDireccion);
        }
	}
}