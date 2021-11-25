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
        private string direccion;
        private string telefono;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }


        public Cliente()
        {
        }

        public Cliente(string nombre, string direcccion, string telefono)
        {
            Id = id;
            Nombre = nombre;
            Direccion = direcccion;
            Telefono = telefono;
        }
    }
}
