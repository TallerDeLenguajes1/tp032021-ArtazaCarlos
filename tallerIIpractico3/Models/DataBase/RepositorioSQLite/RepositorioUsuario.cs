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
                            usuarioTemp.Pass = UsuarioFilas["pass"].ToString();
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
                                                            pass
                                                  
                                                        )
                                                        VALUES(
                                                            @nombre,
                                                            @user,
                                                            @pass
                                               
                                                        )";
            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                    {
                        command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                        command.Parameters.AddWithValue("@user", usuario.User);
                        command.Parameters.AddWithValue("@pass", usuario.Pass);
               
                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
   
            }
        }

        //public Usuario UsuarioByUser(string user)
        //{
        //    Usuario usuarioNull = null;
        //    string queryString = @"SELECT * FROM Usuarios WHERE user = @user;";

        //    try
        //    {
        //        using (var conexion = new SQLiteConnection(connectionString))
        //        {
        //            using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
        //            {
        //                command.Parameters.AddWithValue("@user", user);
        //                conexion.Open();

        //                SQLiteDataReader dataReader = command.ExecuteReader();
        //                Usuario usuario = new Usuario();
        //                while (dataReader.Read())
        //                {
                            
        //                    usuario.Id = Convert.ToInt32(dataReader["usuarioId"]);
        //                    usuario.Nombre = dataReader["nombre"].ToString();
        //                    usuario.User = dataReader["user"].ToString();
        //                    usuario.Pass = dataReader["pass"].ToString();
        //                    usuario.Rol = dataReader["rol"].ToString();
        //                    dataReader.Close();
        //                    conexion.Close();
        //                    return usuario;

        //                }
                        
        //            }

        //        }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.ToString());
        //    }
        //    return usuarioNull;
        //}

        public Usuario UsuarioByUserPass(string user, string pass)
        {
            Usuario usuarioNull = null;
            string queryString = @"SELECT * FROM Usuarios WHERE user = @user AND pass = @pass;";

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
                        Usuario usuario = new Usuario();
                        while (dataReader.Read())
                        {
                            usuario.Id = Convert.ToInt32(dataReader["usuarioId"]);
                            usuario.Nombre = dataReader["nombre"].ToString();
                            usuario.User = dataReader["user"].ToString();
                            usuario.Pass = dataReader["pass"].ToString();
                            usuario.Rol = dataReader["rol"].ToString();
                            dataReader.Close();
                            conexion.Close();
                            return usuario;
                        }
                        
                    }         
                }    
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return usuarioNull;
        }

        public bool DeleteUsuario(int usuarioId)
        {
            string queryString = @"UPDATE Usuarios
                                                SET
                                                    activo = 0
                                                WHERE usuarioId = @usuarioId;";
            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                    {
                        command.Parameters.AddWithValue("@usuarioId", usuarioId);
                        conexion.Open();
                        command.ExecuteNonQuery();
                    }
                    conexion.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return false;
            }
        }

        public void UpdateUsuario(Usuario usuarioUpdate)
        {
            string queryString = @"UPDATE Usuarios
                                                SET
                                                    nombre = @nombre,
                                                    user = @user,
                                                    pass = @pass
                                                WHERE usuarioId = @usuarioId;";

            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                    {
                        command.Parameters.AddWithValue("@nombre", usuarioUpdate.Nombre);
                        command.Parameters.AddWithValue("@user", usuarioUpdate.User);
                        command.Parameters.AddWithValue("@pass", usuarioUpdate.Pass);

                        command.Parameters.AddWithValue("@usuarioId", usuarioUpdate.Id);
                        conexion.Open();
                        command.ExecuteNonQuery();
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
        
            }
        }
    }
}
