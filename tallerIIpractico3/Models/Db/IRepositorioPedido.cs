using System.Collections.Generic;
using tallerIIpractico3.entities;

namespace tallerIIpractico3.Models.Db
{
    public interface IRepositorioPedido
    {
        Pedido PedidoById(int id);
        void DeletePedido(int id);
        List<Pedido> ReadPedidos();
        void SavePedido(Pedido pedido, int cadeteId);
        void UpdatePedido(int pedidoId, string estado);
        List<Pedido> GetPedidosImpagos(int cadeteId);
    }
}