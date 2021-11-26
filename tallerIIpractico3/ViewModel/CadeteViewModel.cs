using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tallerIIpractico3.ViewModel
{
    public enum TipoVehiculo { Bicicleta, Auto, Moto }
    public class CadeteViewModel
    {

        private string nombre;
        private string direccion;
        private string telefono;
        private string vehiculo;
        private int pedidosPendientes;
        private List<PedidoViewModel> pedidos;


        [Required]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required]
        public string Direccion { get => direccion; set => direccion = value; }
        [Required]
        public string Telefono { get => telefono; set => telefono = value; }
        [Required]
        public string Vehiculo { get => vehiculo; set => vehiculo = value; }

        public int PedidosPendientes { get => pedidosPendientes; set => pedidosPendientes = value; }
        public List<PedidoViewModel> Pedidos { get => pedidos; set => pedidos = value; }


        public CadeteViewModel(string nom, string dir, string tel, TipoVehiculo vehiculo)
        {
            Nombre = nom;
            Direccion = dir;
            Telefono = tel;
            Vehiculo = vehiculo.ToString();
            Pedidos = new List<PedidoViewModel>();
            PedidosPendientes = 0;
        }

        public CadeteViewModel()
        {
            Pedidos = new List<PedidoViewModel>();
        }

        public void addOrder(PedidoViewModel ped)
        {
            Pedidos.Add(ped);
        }
        public void removeOrder(PedidoViewModel ped)
        {
            Pedidos.Remove(ped);
        }
    }
}
