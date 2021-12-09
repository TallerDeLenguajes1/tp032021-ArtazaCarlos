using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using tallerIIpractico3.entities;
using NLog;

namespace tallerIIpractico3.Models.Db
{
    public class RepositorioClienteSQLite : IRepositorioCliente
    {
        private readonly string connectionString;
        private readonly Logger logger;

        public RepositorioClienteSQLite(string ConnectionString, Logger logger)
        {
            connectionString = ConnectionString;
            this.logger = logger;
        }

        public void SaveCliente(Cliente cliente)
        {
            string queryString = @"INSERT INTO Clientes (
                                                            nombre,
                                                            direccion,
                                                            telefono
                                                          
                                                        )
                                                        VALUES(
                                                            @nombre,
                                                            @direccion,
                                                            @telefono
                                                          
                                                        )";
            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                    {
                        command.Parameters.AddWithValue("@nombre", cliente.Nombre);
                        command.Parameters.AddWithValue("@direccion", cliente.Direccion);
                        command.Parameters.AddWithValue("@telefono", cliente.Telefono);

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

        public Cliente ClienteById(int id)
        {
            Cliente clienteId = new Cliente();
            string queryString = @"SELECT * FROM Clientes WHERE clienteId = @id;";

            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        conexion.Open();
                        //int i = command.ExecuteNonQuery();
                        //Cadete cadeteTemp = new Cadete();
                        SQLiteDataReader dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            clienteId.Id = Convert.ToInt32(dataReader["clienteId"]);
                            clienteId.Nombre = dataReader["nombre"].ToString();
                            clienteId.Direccion = dataReader["direccion"].ToString();
                            clienteId.Telefono = dataReader["telefono"].ToString();

                        }

                    }
                    conexion.Close();
                }
                return clienteId;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                throw;
            }
        }

        public Cliente ClienteByNomTel(string nom, string tel)
        {
            Cliente cliente = new Cliente();
            string queryString = @"SELECT * FROM Clientes WHERE nombre = @nombre AND telefono = @telefono;";

            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                    {
                        command.Parameters.AddWithValue("@nombre", nom);
                        command.Parameters.AddWithValue("@telefono", tel);
                        conexion.Open();
                        //int i = command.ExecuteNonQuery();
                        //Cadete cadeteTemp = new Cadete();
                        SQLiteDataReader dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            cliente.Id = Convert.ToInt32(dataReader["clienteId"]);
                            cliente.Nombre = dataReader["nombre"].ToString();
                            cliente.Direccion = dataReader["direccion"].ToString();
                            cliente.Telefono = dataReader["telefono"].ToString();

                        }

                    }
                    conexion.Close();
                }
                return cliente;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                throw;
            }
        }

        //quizas no hace falta
        public int GetClienteId(string nom, string tel)
        {
            int clienteId = 0;

            string queryString = @"SELECT clienteId FROM Clientes WHERE nombre = @nombre AND telefono = @telefono;";

            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                    {
                        command.Parameters.AddWithValue("@nombre", nom);
                        command.Parameters.AddWithValue("@telefono", tel);
                        conexion.Open();
                        SQLiteDataReader dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            clienteId = Convert.ToInt32(dataReader["clienteId"]);

                        }

                    }
                    conexion.Close();
                }
                return clienteId;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                throw;
            }

        }

        public bool DeleteCliente(int clienteId)
        {
            string queryString = @"UPDATE Clientes
                                                SET
                                                    activo = 0
                                                WHERE clienteId = @clienteId;";
            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                    {
                        command.Parameters.AddWithValue("@clienteId", clienteId);
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


        public List<Cliente> ReadCliente()
        {
            List<Cliente> ListaDeClientes = new List<Cliente>();
            string queryString = @"SELECT
                                        clienteId,
                                        nombre,
                                        direccion,
                                        telefono
                                  
                                FROM Clientes WHERE activo = 1;";
            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                    {
                        conexion.Open();
                        SQLiteDataReader clienteFilas = command.ExecuteReader();
                        while (clienteFilas.Read())
                        {
                            Cliente clienteTemp = new Cliente();
                            clienteTemp.Id = Convert.ToInt32(clienteFilas["clienteId"]);
                            clienteTemp.Nombre = clienteFilas["nombre"].ToString();
                            clienteTemp.Direccion = clienteFilas["direccion"].ToString();
                            clienteTemp.Telefono = clienteFilas["telefono"].ToString();
                          
                            ListaDeClientes.Add(clienteTemp);
                        }
                        clienteFilas.Close();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());

            }
            return ListaDeClientes;
        }

        public void UpdateCliente(Cliente clienteUpdate)
        {
            string queryString = @"UPDATE Clientes
                                                SET
                                                    nombre = @nombre,
                                                    direccion = @direccion,
                                                    telefono = @telefono
                                                WHERE clienteId = @Id;";

            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                    {
                        command.Parameters.AddWithValue("@nombre", clienteUpdate.Nombre);
                        command.Parameters.AddWithValue("@direccion", clienteUpdate.Direccion);
                        command.Parameters.AddWithValue("@telefono", clienteUpdate.Telefono);

                        command.Parameters.AddWithValue("@Id", clienteUpdate.Id);
                        conexion.Open();
                        command.ExecuteNonQuery();
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                throw;
            }
        }
    }
}
