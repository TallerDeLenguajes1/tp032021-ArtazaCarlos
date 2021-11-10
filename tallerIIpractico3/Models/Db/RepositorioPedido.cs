using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using tallerIIpractico3.entities;

namespace tallerIIpractico3.Models.Db
{
    public class RepositorioPedido
    {
        private readonly string connectionString;

        public RepositorioPedido(string ConnectionString)
        {
            connectionString = ConnectionString;
        }

        public List<Pedido> ReadPedidos()
        {
            List<Pedido> ListaDePedidos = new List<Pedido>();
            string queryString = @"SELECT * FROM Pedidos INNER JOIN Clientes 
                                    USING(clienteId) 
                                    WHERE Pedidos.activo = 1 AND Clientes.activo = 1";

            using (var conexion = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    SQLiteDataReader PedidoFilas = command.ExecuteReader();
                    while (PedidoFilas.Read())
                    {
                        Pedido PedidoTemp = new Pedido();
                        Cliente ClienteTemp = new Cliente();

                        PedidoTemp.Id = Convert.ToInt32(PedidoFilas["pedidoId"]);
                        PedidoTemp.Fecha = Convert.ToDateTime(PedidoFilas["fecha"]);
                        PedidoTemp.Observaciones = Convert.ToString(PedidoFilas["observaciones"]);
                        PedidoTemp.Estado = Convert.ToString(PedidoFilas["estado"]);

                        ClienteTemp.Id = Convert.ToInt32(PedidoFilas["pedidoId"]);
                        ClienteTemp.Nombre = Convert.ToString(PedidoFilas["nombre"]);
                        ClienteTemp.Direcccion = Convert.ToString(PedidoFilas["direccion"]);
                        ClienteTemp.Telefono = Convert.ToString(PedidoFilas["telefono"]);

                        PedidoTemp.Cliente = ClienteTemp;
                        ListaDePedidos.Add(PedidoTemp);
                    }

                    conexion.Close();
                }
            }

            return ListaDePedidos;
        }
    }
}
