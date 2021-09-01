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
        private List<Cadete> cadetes;

        public string Nombre { get => nombre; set => nombre = value; }
        

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
