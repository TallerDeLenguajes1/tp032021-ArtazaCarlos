using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace tallerIIpractico3.Models.Db
{
    public class Db
    {
        private IRepositorioCliente clienteDb;
        private IRepositorioCadete cadeteDb;
        private IRepositorioPedido pedidoDb;
        

        public Db(IRepositorioCadete repositorioCadete, IRepositorioPedido repositorioPedido, IRepositorioCliente repositorioCliente)
        {
            CadeteDb = repositorioCadete;
            PedidoDb = repositorioPedido;
            ClienteDb = repositorioCliente;
        }

        public IRepositorioCadete CadeteDb { get => cadeteDb; set => cadeteDb = value; }
        public IRepositorioPedido PedidoDb { get => pedidoDb; set => pedidoDb = value; }
        public IRepositorioCliente ClienteDb { get => clienteDb; set => clienteDb = value; }
    }
}
