using System;
using System.ComponentModel.DataAnnotations;

namespace tallerIIpractico3.ViewModel
{
    public enum Estado
    {
        Sin_asignar,
        En_camino,
        Entregado,
        No_entregado,
    }
    public class PedidoViewModel
    {

        private string fecha;
        private string observaciones;
        private ClienteViewModel cliente;
        private string estadoPedido;



        public string Fecha { get => fecha; set => fecha = value; }
        [Required]
        public string Observaciones { get => observaciones; set => observaciones = value; }

        public ClienteViewModel Cliente { get => cliente; set => cliente = value; }
        public string EstadoPedido { get => estadoPedido; set => estadoPedido = value; }

        public PedidoViewModel()
        {
        }

        public PedidoViewModel(string observaciones, ClienteViewModel cliente)
        {
    
            Fecha = DateTime.Now.ToString();
            Observaciones = observaciones;
            Cliente = cliente;
            EstadoPedido = Estado.En_camino.ToString();
        }
    }
}
