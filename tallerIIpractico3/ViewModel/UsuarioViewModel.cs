using System.ComponentModel.DataAnnotations;

namespace tallerIIpractico3.ViewModel
{
    public class UsuarioViewModel
    {
        private int id;
        private string nombre;
        private string user;
        private string pass;
        private string rol;


        public int Id { get => id; set => id = value; }
        [Required]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required]
        public string User { get => user; set => user = value; }
        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        [Display(Name = "Contraseña")]
        public string Pass { get => pass; set => pass = value; }
        public string Rol { get => rol; set => rol = value; }


        public UsuarioViewModel()
        {
        }
    }
}
