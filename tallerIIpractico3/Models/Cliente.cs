using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tallerIIpractico3.entities
{
    public class Cliente
    {
        private int id;
        private string nombre;
        private string direcccion;
        private string telefono;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direcccion { get => direcccion; set => direcccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }

        public Cliente(int dni, string nom, string dir, string tel)
        {
            Id = dni;
            Nombre = nom;
            Direcccion = dir;
            Telefono = tel;
        }

        public Cliente()
        {
        }
    }
}
