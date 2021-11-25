using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tallerIIpractico3.entities
{   
    public enum Estado
    {
        Sin_asignar,
        En_camino,
        Entregado,
        No_entregado,
    }
    public class Pedido
    {
        private int id;
        private string fecha;
        private string observaciones;
        private Cliente cliente;
        private string estadoPedido;


        public int Id { get => id; set => id = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string Observaciones { get => observaciones; set => observaciones = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public string EstadoPedido { get => estadoPedido; set => estadoPedido = value; }

        public Pedido()
        {
        }

        public Pedido(string observaciones, Cliente cliente)
        {
            Id = 9999;
            Fecha = DateTime.Now.ToString();
            Observaciones = observaciones;
            Cliente = cliente;
            EstadoPedido = Estado.En_camino.ToString();
        }

        public Pedido(string observaciones)
        {
            Id = 9999;
            Fecha = DateTime.Now.ToString();
            Observaciones = observaciones;
            EstadoPedido = Estado.En_camino.ToString();
        }
    }
}
