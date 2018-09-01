using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuEscuela.Models
{
    public class Alumno
    {
        public int IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string CURP { get; set; }
        public string RFC { get; set; }
        public string NSS { get; set; }
        public string Correo { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string NumControl { get; set; }
        public byte[] imagen { get; set; }
        public int Edad {
            get {
                try
                {
                    Int32 age = -1;
                    if (FechaNacimiento != new DateTime(1, 1, 1))
                    {
                        var today = DateTime.Today;
                        age = today.Year - FechaNacimiento.Year;
                        if (FechaNacimiento > today.AddYears(-age)) age--;
                    }
                    return age;
                }
                catch(Exception)
                {
                    return -1;
                }
            }
        }


    }
}