using System.ComponentModel.DataAnnotations;

namespace tallerIIpractico3.ViewModel
{
    public class ClienteViewModel
    {

        private string nombre;
        private string direccion;
        private string telefono;

        [Required]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required]
        public string Direccion { get => direccion; set => direccion = value; }
        [Required]
        public string Telefono { get => telefono; set => telefono = value; }


        public ClienteViewModel()
        {
        }

        public ClienteViewModel(string nombre, string direcccion, string telefono)
        {
     
            Nombre = nombre;
            Direccion = direcccion;
            Telefono = telefono;
        }
    }
}
