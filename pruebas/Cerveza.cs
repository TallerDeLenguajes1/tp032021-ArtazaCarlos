using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pruebas
{
    class Cerveza
    {
        int cantidad;
        string marca;

        public int Cantidad { get => cantidad; set => cantidad = value; }
        public string Marca { get => marca; set => marca = value; }

        public Cerveza()
        {
        }

        public Cerveza(int cant, string marc)
        {
            Cantidad = cant;
            Marca = marc;
        }
    }
}
