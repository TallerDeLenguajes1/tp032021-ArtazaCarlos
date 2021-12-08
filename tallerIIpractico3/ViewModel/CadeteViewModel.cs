using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tallerIIpractico3.ViewModel
{
    public enum TipoVehiculo { Bicicleta, Auto, Moto } 

    public class CadeteViewModel
    {
        private int id;
        private string nombre;
        private string direccion;
        private string telefono;
        private string vehiculo;

        private List<PedidoViewModel> pedidos;


        public int Id { get => id; set => id = value; }
        [Required]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required]
        public string Direccion { get => direccion; set => direccion = value; }
        [Required]
        public string Telefono { get => telefono; set => telefono = value; }
        [Required]
        public string Vehiculo { get => vehiculo; set => vehiculo = value; }


        public List<PedidoViewModel> Pedidos { get => pedidos; set => pedidos = value; }
        

        public CadeteViewModel()
        {
        }


    }
}
