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
        private IRepositorioUsuario usuarioDb;

        public Db(IRepositorioCadete repositorioCadete, IRepositorioPedido repositorioPedido, IRepositorioCliente repositorioCliente, IRepositorioUsuario RepositorioUsuario)
        {
            CadeteDb = repositorioCadete;
            PedidoDb = repositorioPedido;
            ClienteDb = repositorioCliente;
            UsuarioDb = RepositorioUsuario;
        }

        public IRepositorioCadete CadeteDb { get => cadeteDb; set => cadeteDb = value; }
        public IRepositorioPedido PedidoDb { get => pedidoDb; set => pedidoDb = value; }
        public IRepositorioCliente ClienteDb { get => clienteDb; set => clienteDb = value; }
        public IRepositorioUsuario UsuarioDb { get => usuarioDb; set => usuarioDb = value; }
    }
}
