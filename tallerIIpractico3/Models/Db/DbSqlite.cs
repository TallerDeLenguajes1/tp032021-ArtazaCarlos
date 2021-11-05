using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tallerIIpractico3.Models.Db
{
    public class DbSqlite
    {
        private RepositorioCadete cadeteDb;
        private RepositorioPedido pedidoDb;
        public DbSqlite(string ConnectionString)
        {
            CadeteDb = new RepositorioCadete(ConnectionString);
            PedidoDb = new RepositorioPedido(ConnectionString);
        }

        public RepositorioCadete CadeteDb { get => cadeteDb; set => cadeteDb = value; }
        public RepositorioPedido PedidoDb { get => pedidoDb; set => pedidoDb = value; }
    }
}
