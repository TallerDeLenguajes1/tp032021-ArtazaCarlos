using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tallerIIpractico3.ViewModel
{
    public class ClienteViewModel
    {
        private int id;
        private string nombre;
        private string direccion;
        private string telefono;
   


        public int Id { get => id; set => id = value; }
        [Required]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required]
        public string Direccion { get => direccion; set => direccion = value; }
        [Required]
        public string Telefono { get => telefono; set => telefono = value; }
  

        public ClienteViewModel()
        {
        }
    }

    public class ClienteIndexViewModel
    {
        private List<ClienteViewModel> clientes;
        private UsuarioViewModel userLog;

        public List<ClienteViewModel> Clientes { get => clientes; set => clientes = value; }
        public UsuarioViewModel UserLog { get => userLog; set => userLog = value; }

        public ClienteIndexViewModel()
        {
        }
    }

    public class ClienteABMViewModel
    {
        private ClienteViewModel cliente;
        private UsuarioViewModel userLog;

        public ClienteViewModel Cliente { get => cliente; set => cliente = value; }
        public UsuarioViewModel UserLog { get => userLog; set => userLog = value; }

        public ClienteABMViewModel()
        {
        }
    }

}
