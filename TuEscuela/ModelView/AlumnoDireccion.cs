using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuEscuela.Models;

namespace TuEscuela.ModelView
{
    public class AlumnoDireccion
    {
        public Alumno Alumno { get; set; }

        public int IdDireccion { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public int IdMunicipio { get; set; }
        public string CodigoPostal { get; set; }
        public string NoExt { get; set; }
        public string NoInt { get; set; }
        public string Municipio { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public List<Contacto> Contactos { get; set; }
    }
}