using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuEscuela.Models
{
    public class DetallePlanEstudios
    {
        public int IdCarrera { get; set; }
        public string NombreCarrera { get; set; }
        public int Niveles { get; set; }
        public string TipoNivel { get; set; }
        public int IdMateria { get; set; }
        public int Nivel { get; set; }
        public string NombreMateria { get; set; }
        public int Creditos { get; set; }
    }
}