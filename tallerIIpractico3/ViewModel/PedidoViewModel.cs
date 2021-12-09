using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tallerIIpractico3.ViewModel
{

    public class PedidoViewModel
    {
        private int id;
        private string fecha;
        private string observaciones;
        private ClienteViewModel cliente;
        private string estadoPedido;
        private int pagado;

        public int Id { get => id; set => id = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        [Required]
        public string Observaciones { get => observaciones; set => observaciones = value; }
        public ClienteViewModel Cliente { get => cliente; set => cliente = value; }
        public string EstadoPedido { get => estadoPedido; set => estadoPedido = value; }
        public int Pagado { get => pagado; set => pagado = value; }

        public PedidoViewModel()
        {
        }
    }

    public class CreatePedidoViewModel
    {
        private List<CadeteViewModel> cadetes;
        private ClienteViewModel cliente;
        private string pedidoObs;
        private int cadeteId;


        public List<CadeteViewModel> Cadetes { get => cadetes; set => cadetes = value; }
        public ClienteViewModel Cliente { get => cliente; set => cliente = value; }
        [Required]
        public string PedidoObs { get => pedidoObs; set => pedidoObs = value; }
        [Required]
        public int CadeteId { get => cadeteId; set => cadeteId = value; }

        public CreatePedidoViewModel()
        {

        }


    }



}
