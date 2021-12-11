using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tallerIIpractico3.ViewModel
{

    public class PedidoViewModel
    {
        private int id;
        private DateTime fecha;
        private string observaciones;
        private ClienteViewModel cliente;
        private string estadoPedido;
        private int pagado;
  

        public int Id { get => id; set => id = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        [Required(ErrorMessage = "Este Campo es requerido")]
        public string Observaciones { get => observaciones; set => observaciones = value; }
        public ClienteViewModel Cliente { get => cliente; set => cliente = value; }
        public string EstadoPedido { get => estadoPedido; set => estadoPedido = value; }
        public int Pagado { get => pagado; set => pagado = value; }


        public PedidoViewModel()
        {
        }
    }

    public class PedidoIndexViewModel
    {
        private List<PedidoViewModel> pedidos;
        private UsuarioViewModel userLog;

        public List<PedidoViewModel> Pedidos { get => pedidos; set => pedidos = value; }
        public UsuarioViewModel UserLog { get => userLog; set => userLog = value; }

        public PedidoIndexViewModel()
        {
        }
    }


    public class CreatePedidoViewModel
    {
        private List<CadeteViewModel> cadetes;
        private ClienteViewModel cliente;
        private string pedidoObs;
        private int cadeteId;
        private UsuarioViewModel userLog;

        public List<CadeteViewModel> Cadetes { get => cadetes; set => cadetes = value; }
        [Required]
        public ClienteViewModel Cliente { get => cliente; set => cliente = value; }
        [Required]
        public string PedidoObs { get => pedidoObs; set => pedidoObs = value; }
        [Required]
        public int CadeteId { get => cadeteId; set => cadeteId = value; }
        public UsuarioViewModel UserLog { get => userLog; set => userLog = value; }

        public CreatePedidoViewModel()
        {
        }
    }



}
