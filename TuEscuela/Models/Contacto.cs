using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuEscuela.Models
{
    public class Contacto
    {
        public int IdContacto { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoRelacion { get; set; }
    }
}