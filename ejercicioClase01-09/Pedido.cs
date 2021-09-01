using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejercicioClase01_09
{
    class Pedido
    {
        int nro;
        string obs;
        string cliente;
        string estado;

        public int Nro { get => nro; set => nro = value; }
        public string Obs { get => obs; set => obs = value; }
        public string Cliente { get => cliente; set => cliente = value; }
        public string Estado { get => estado; set => estado = value; }
    }
}
