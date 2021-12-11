
namespace tallerIIpractico3.Models.Entities
{
    public enum RolUsuario
    {
        admin,
        user
    }

    public class Usuario
    {
        private int id;
        private string nombre;
        private string user;
        private string pass;
        private string rol;

        
        public string Nombre { get => nombre; set => nombre = value; }
        public string User { get => user; set => user = value; }
        public string Pass { get => pass; set => pass = value; }
        public string Rol { get => rol; set => rol = value; }
        public int Id { get => id; set => id = value; }

        public Usuario()
        {
        }

        public Usuario(int id, string nombre, string user, string pass, RolUsuario rol)
        {
            Id = id;
            Nombre = nombre;
            User = user;
            Pass = pass;
            Rol = rol.ToString();
        }
    }
}
