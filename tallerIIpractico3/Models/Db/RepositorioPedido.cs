using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tallerIIpractico3.Models.Db
{
    public class RepositorioPedido
    {
        private readonly string connectionString;

        public RepositorioPedido(string ConnectionString)
        {
            connectionString = ConnectionString;
        }
    }
}
