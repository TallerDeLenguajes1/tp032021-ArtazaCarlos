using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tallerIIpractico3.entities
{
    public class Cadeteria
    {
        public List<Cadete> Cadetes { get; set; }
        public List<Pedido> Pedidos { get; set; }

        public Cadeteria()
        {
            Cadetes = new List<Cadete>();
            Pedidos = new List<Pedido>();

            


        }
    }
}
