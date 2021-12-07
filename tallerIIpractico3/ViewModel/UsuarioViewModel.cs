using System.ComponentModel.DataAnnotations;

namespace tallerIIpractico3.ViewModel
{
    public class UsuarioViewModel
    {
        private string nombre;
        private string user;
        private string password;


        [Required]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required]
        public string User { get => user; set => user = value; }
        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        [Display(Name = "Contraseña")]
        public string Password { get => password; set => password = value; }
 

        public UsuarioViewModel()
        {
        }
    }
}
