using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejercicioClase01_09
{
    class Cliente
    {
        private int id;
        private string nombre;
        private string direcccion;
        private int telefono;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direcccion { get => direcccion; set => direcccion = value; }
        public int Telefono { get => telefono; set => telefono = value; }

        public Cliente(int dni, string nom, string dir, int tel)
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
