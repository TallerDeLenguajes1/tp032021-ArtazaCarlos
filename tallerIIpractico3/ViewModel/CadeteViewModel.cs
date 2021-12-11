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
        [Required(ErrorMessage = "Este Campo es requerido")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required(ErrorMessage = "Este Campo es requerido")]
        public string Direccion { get => direccion; set => direccion = value; }
        [Required(ErrorMessage = "Este Campo es requerido")]
        public string Telefono { get => telefono; set => telefono = value; }
        [Required(ErrorMessage = "Este Campo es requerido")]
        public string Vehiculo { get => vehiculo; set => vehiculo = value; }


        public List<PedidoViewModel> Pedidos { get => pedidos; set => pedidos = value; }


        public CadeteViewModel()
        {
        }
    }

    public class CadeteIndexViewModel
    {
        private List<CadeteViewModel> cadetes;
        private UsuarioViewModel userLog;

        public List<CadeteViewModel> Cadetes { get => cadetes; set => cadetes = value; }
        public UsuarioViewModel UserLog { get => userLog; set => userLog = value; }

        public CadeteIndexViewModel()
        {
        }
    }

    public class CadeteABMViewModel
    {
        private CadeteViewModel cadete;
        private UsuarioViewModel userLog;

        public CadeteViewModel Cadete { get => cadete; set => cadete = value; }
        public UsuarioViewModel UserLog { get => userLog; set => userLog = value; }

        public CadeteABMViewModel()
        {
        }
    }

}
