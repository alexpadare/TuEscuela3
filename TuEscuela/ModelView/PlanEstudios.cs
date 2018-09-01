using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TuEscuela.Models;

namespace TuEscuela.ModelView
{
    public class PlanEstudios
    {
        public int IdCarrera { get; set; }
        public string NombreCarrera { get; set; }
        public List<NivelCarrera> Niveles { get; set; }
        public PlanEstudios()
        {
        }
    }

    public class NivelCarrera
    {
        public int Nivel { get; set; }
        public string TipoNivel { get; set; }
        public List<Materia> Materias {get; set;}
    }
}