using System;
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
        bool UpdatePedido(int pedidoId, string estado);
        bool LiquidarPedido(int cadeteId);
        List<Pedido> GetPedidosImpagos(int cadeteId);
        List<Pedido> BusquedaFiltradaPorFecha(DateTime fechaInicial, DateTime fechaFinal);
    }
}