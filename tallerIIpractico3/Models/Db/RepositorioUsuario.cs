using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using tallerIIpractico3.entities;
using NLog;
using tallerIIpractico3.Models.Entities;

namespace tallerIIpractico3.Models.Db
{
    public class RepositorioUsuarioSQLite : IRepositorioUsuario
    {
        private readonly string connectionString;
        private readonly Logger logger;

        public RepositorioUsuarioSQLite(string ConnectionString, Logger logger)
        {
            connectionString = ConnectionString;
            this.logger = logger;
        }

        public List<Usuario> ReadUsuarios()
        {
            List<Usuario> ListaDeUsuario = new List<Usuario>();
            string queryString = @"SELECT * FROM Usuarios WHERE activo = 1;";

            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                    {
                        conexion.Open();
                        SQLiteDataReader UsuarioFilas = command.ExecuteReader();
                        while (UsuarioFilas.Read())
                        {
                            Usuario usuarioTemp = new Usuario();
                            usuarioTemp.Id = Convert.ToInt32(UsuarioFilas["usuarioId"]);
                            usuarioTemp.Nombre = UsuarioFilas["nombre"].ToString();
                            usuarioTemp.User = UsuarioFilas["user"].ToString();
                            usuarioTemp.Password = UsuarioFilas["password"].ToString();
                            usuarioTemp.Rol = UsuarioFilas["rol"].ToString();

                            ListaDeUsuario.Add(usuarioTemp);
                        }
                        UsuarioFilas.Close();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());

            }
            return ListaDeUsuario;
        }

        public void SaveUsuario(Usuario usuario)
        {
            string queryString = @"INSERT INTO Usuarios (
                                                            nombre,
                                                            user,
                                                            password
                                                  
                                                        )
                                                        VALUES(
                                                            @nombre,
                                                            @user,
                                                            @password
                                               
                                                        )";
            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                    {
                        command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                        command.Parameters.AddWithValue("@user", usuario.User);
                        command.Parameters.AddWithValue("@password", usuario.Password);
               
                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                throw;
            }
        }

        public Usuario UsuarioByUserPass(string user, string pass)
        {
            Usuario usuarioNull = null;
            string queryString = @"SELECT * FROM Usuarios WHERE user = @user AND password = @pass;";

            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                    {
                        command.Parameters.AddWithValue("@user", user);
                        command.Parameters.AddWithValue("@pass", pass);
                        conexion.Open();
               
                        SQLiteDataReader dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            Usuario usuario = new Usuario();
                            usuario.Id = Convert.ToInt32(dataReader["usuarioId"]);
                            usuario.Nombre = dataReader["nombre"].ToString();
                            usuario.User = dataReader["user"].ToString();
                            usuario.Password = dataReader["password"].ToString();
                            usuario.Rol = dataReader["rol"].ToString();

                            return usuario;
                        }
                        dataReader.Close();
                        conexion.Close();
                    }
                      
                }
                return usuarioNull;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                throw;
            }
        }
    }
}
