using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examenParcial1
{
    class Registros
    {
        string codigo;
        List<string> medicion;
        List<string> fechaMedicion;

        public string Codigo { get => codigo; set => codigo = value; }
        public List<string> Medicion { get => medicion; set => medicion = value; }
        public List<string> FechaMedicion { get => fechaMedicion; set => fechaMedicion = value; }

        public Registros()
        {
            Codigo = "";
            Medicion = new List<string>();
            FechaMedicion = new List<string>();
        }
    }
}
