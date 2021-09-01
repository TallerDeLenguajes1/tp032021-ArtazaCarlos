using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejercicioClase01_09
{
    class Cliente
    {
        int id;
        string nombre;
        string direcccion;
        int telefono;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direcccion { get => direcccion; set => direcccion = value; }
        public int Telefono { get => telefono; set => telefono = value; }
    }
}
