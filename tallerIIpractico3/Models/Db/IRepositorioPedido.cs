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
        void LiquidarPedido(int cadeteId);
        List<Pedido> GetPedidosImpagos(int cadeteId);
    }
}