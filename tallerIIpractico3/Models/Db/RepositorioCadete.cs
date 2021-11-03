using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tallerIIpractico3.Models.Db
{
    public class RepositorioCadete
    {
        private readonly string connectionString;

        public RepositorioCadete(string ConnectionString)
        {
            connectionString = ConnectionString;
        }
    }
}
