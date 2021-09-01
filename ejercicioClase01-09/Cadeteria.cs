using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejercicioClase01_09
{
    class Cadeteria
    {
        string nombre;
        List<Cadete> cadetes;

        public string Nombre { get => nombre; set => nombre = value; }
        internal List<Cadete> Cadetes { get => cadetes; set => cadetes = value; }

        public Cadeteria(string Nom)
        {
            Nombre = Nom;
        }
        public void agregarCadete(Cadete cad)
        {
            cadetes.Add(cad);
        }
    }
}
