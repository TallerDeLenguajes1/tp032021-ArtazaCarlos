using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using tallerIIpractico3.entities;
using NLog;

namespace tallerIIpractico3.Models.Db
{
    public class RepositorioPedidoSQLite : IRepositorioPedido
    {
        private readonly string connectionString;
        private readonly Logger logger;

        public RepositorioPedidoSQLite(string ConnectionString, Logger logger)
        {
            connectionString = ConnectionString;
            this.logger = logger;
        }

        public List<Pedido> ReadPedidos()
        {
            List<Pedido> ListaDePedidos = new List<Pedido>();
            string queryString = @"SELECT * FROM Pedidos INNER JOIN Clientes 
                                    USING(clienteId) 
                                    WHERE Pedidos.activo = 1";
            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                    {
                        conexion.Open();
                        SQLiteDataReader PedidoFilas = command.ExecuteReader();
                        while (PedidoFilas.Read())
                        {
                            Pedido PedidoTemp = new Pedido();
                            Cliente ClienteTemp = new Cliente();

                            PedidoTemp.Id = Convert.ToInt32(PedidoFilas["pedidoId"]);
                            PedidoTemp.Fecha = Convert.ToString(PedidoFilas["fecha"]);
                            PedidoTemp.Observaciones = Convert.ToString(PedidoFilas["observaciones"]);
                            PedidoTemp.EstadoPedido = Convert.ToString(PedidoFilas["estadoPedido"]);

                            ClienteTemp.Id = Convert.ToInt32(PedidoFilas["clienteId"]);
                            ClienteTemp.Nombre = Convert.ToString(PedidoFilas["nombre"]);
                            ClienteTemp.Direccion = Convert.ToString(PedidoFilas["direccion"]);
                            ClienteTemp.Telefono = Convert.ToString(PedidoFilas["telefono"]);

                            PedidoTemp.Cliente = ClienteTemp;
                            ListaDePedidos.Add(PedidoTemp);
                        }
                        PedidoFilas.Close();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            
            }
            return ListaDePedidos;
        }

        public void SavePedido(Pedido pedido, int cadeteId)
        {
            string queryString = @"INSERT INTO Pedidos (
                                                fecha,
                                                observaciones,
                                                clienteId,
                                                cadeteId,
                                                estadoPedido
                                            )
                                            VALUES (
                                                @fecha,
                                                @observaciones,
                                                @clienteId,
                                                @cadeteId,
                                                @estadoPedido
                                            );";
            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    
                    
                    using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                    {
                        command.Parameters.AddWithValue("@fecha", pedido.Fecha);
                        command.Parameters.AddWithValue("@observaciones", pedido.Observaciones);
                        command.Parameters.AddWithValue("@clienteId", pedido.Cliente.Id);
                        command.Parameters.AddWithValue("@cadeteId", cadeteId);
                        command.Parameters.AddWithValue("@estadoPedido", pedido.EstadoPedido);
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

        public void UpdatePedido(int pedidoId, string estado)
        {
            string queryString = @"UPDATE Pedidos
                                                SET
                                                    estadoPedido = @estado
                                                WHERE pedidoId = @Id;";

            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                    {
                        command.Parameters.AddWithValue("@estado", estado);
                        command.Parameters.AddWithValue("@Id", pedidoId);
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

        public void DeletePedido(int id)
        {
            throw new NotImplementedException();
        }

        public Pedido PedidoById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Pedido> GetPedidosImpagos(int cadeteId)
        {
            List<Pedido> ListaDePedidos = new List<Pedido>();
            string queryString = @"SELECT * FROM Pedidos INNER JOIN Clientes 
                                    USING(clienteId) 
                                    WHERE Pedidos.activo = 1 AND Pedidos.pagado = 0 AND Pedidos.cadeteId = @cadeteId";
            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                    {
                        conexion.Open();
                        command.Parameters.AddWithValue("@cadeteId", cadeteId);
                        SQLiteDataReader PedidoFilas = command.ExecuteReader();
                        while (PedidoFilas.Read())
                        {
                            Pedido PedidoTemp = new Pedido();
                            Cliente ClienteTemp = new Cliente();

                            PedidoTemp.Id = Convert.ToInt32(PedidoFilas["pedidoId"]);
                            PedidoTemp.Fecha = Convert.ToString(PedidoFilas["fecha"]);
                            PedidoTemp.Observaciones = Convert.ToString(PedidoFilas["observaciones"]);
                            PedidoTemp.EstadoPedido = Convert.ToString(PedidoFilas["estadoPedido"]);

                            ClienteTemp.Id = Convert.ToInt32(PedidoFilas["clienteId"]);
                            ClienteTemp.Nombre = Convert.ToString(PedidoFilas["nombre"]);
                            ClienteTemp.Direccion = Convert.ToString(PedidoFilas["direccion"]);
                            ClienteTemp.Telefono = Convert.ToString(PedidoFilas["telefono"]);

                            PedidoTemp.Cliente = ClienteTemp;
                            ListaDePedidos.Add(PedidoTemp);
                        }
                        PedidoFilas.Close();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());

            }
            return ListaDePedidos;
        }

        public void LiquidarPedido(int cadeteId)
        {
            string queryString = @"UPDATE Pedidos
                                                SET
                                                    pagado = 1
                                                WHERE 
                                                    cadeteId = @cadeteId 
                                                    AND activo = 1 
                                                    AND estadoPedido != 'En_camino';";

            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                    {
                        command.Parameters.AddWithValue("@cadeteId", cadeteId);
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
