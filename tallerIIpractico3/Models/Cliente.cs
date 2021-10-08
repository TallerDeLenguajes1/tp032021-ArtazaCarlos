using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tallerIIpractico3.entities
{
    public class Cliente
    {
        private string nombre;
        private string direcccion;
        private string telefono;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Direcccion { get => direcccion; set => direcccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }

        public Cliente(string nom, string dir, string tel)
        {
            Nombre = nom;
            Direcccion = dir;
            Telefono = tel;
        }
        public Cliente()
        {
        }
    }
}
