using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejercicioClase01_09
{
    class Cadeteria
    {
        private string nombre;
        private List<Cadete> cadetes;

        public string Nombre { get => nombre; set => nombre = value; }
        

        public Cadeteria(string Nom)
        {
            Nombre = Nom;
        }

        public Cadeteria()
        {
        }

        public void addCadete(Cadete cad)
        {
            cadetes.Add(cad);
        }
        public void removeCadete(Cadete cad)
        {
            cadetes.Remove(cad);
        }
    }
}
