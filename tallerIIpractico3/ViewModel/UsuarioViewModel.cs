using System.Collections.Generic;
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
        [Required(ErrorMessage = "Este Campo es requerido")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required(ErrorMessage = "Este Campo es requerido")]
        public string User { get => user; set => user = value; }
        [Required(ErrorMessage = "Este Campo es requerido")]
        [MinLength(6)]
        [MaxLength(20)]
        [Display(Name = "Contraseña")]
        public string Pass { get => pass; set => pass = value; }
        public string Rol { get => rol; set => rol = value; }


        public UsuarioViewModel()
        {
        }
    }

    public class UsuarioListaiewModel
    {
        private List<UsuarioViewModel> usuarios;
        private UsuarioViewModel userLog;

        public List<UsuarioViewModel> Usuarios { get => usuarios; set => usuarios = value; }
        public UsuarioViewModel UserLog { get => userLog; set => userLog = value; }

        public UsuarioListaiewModel()
        {
        }
    }


    public class UsuarioABMViewModel
    {
        private UsuarioViewModel usuario;
        private UsuarioViewModel userLog;

        public UsuarioViewModel Usuario { get => usuario; set => usuario = value; }
        public UsuarioViewModel UserLog { get => userLog; set => userLog = value; }

        public UsuarioABMViewModel()
        {
        }
    }
}
