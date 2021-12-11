using System.Collections.Generic;
using tallerIIpractico3.Models.Entities;

namespace tallerIIpractico3.Models.Db
{
    public interface IRepositorioUsuario
    {
        List<Usuario> ReadUsuarios();
        void SaveUsuario(Usuario cadete);
        Usuario UsuarioByUserPass(string user, string pass);
        //Usuario UsuarioByUser(string user);
        bool DeleteUsuario(int usuarioId);
        void UpdateUsuario(Usuario usuarioUpdate);
    }
}