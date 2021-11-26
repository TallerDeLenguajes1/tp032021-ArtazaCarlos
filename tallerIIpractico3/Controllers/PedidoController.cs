using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using tallerIIpractico3.entities;
using tallerIIpractico3.Models.Db;
using NLog;

namespace tallerIIpractico3.Controllers
{
    public class PedidoController : Controller
    {
        private readonly Db db;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static DateTime fechaInicial;
        private static DateTime fechaFinal;

        public static DateTime FechaInicial { get => fechaInicial; set => fechaInicial = value; }
        public static DateTime FechaFinal { get => fechaFinal; set => fechaFinal = value; }

        public PedidoController(Db Db)
        {
            db = Db;
        
        }

        public IActionResult Index()
        {
            return View(db.PedidoDb.ReadPedidos());
        }

        public IActionResult CreateView()
        {
            return View(db.CadeteDb.ReadCadetes());
        }

        public IActionResult CreatePedido(string obs, string nom, string dir, string tel, int cadeteId)
        {
            Cliente cliente = new(nom, dir, tel);
            db.ClienteDb.SaveCliente(cliente);
            Cliente clienteWithId = db.ClienteDb.ClienteByNomTel(nom, tel);
            Pedido pedido = new(obs, clienteWithId);
            db.PedidoDb.SavePedido(pedido, cadeteId);

            return RedirectToAction("Index");
        }

        public IActionResult UpdatePedido(int pedidoId, Estado estadoPedido)
        {
            db.PedidoDb.UpdatePedido(pedidoId, estadoPedido.ToString());
            return RedirectToAction("Index");
        }




        ////modifica el pedido desde un listado completo de pedidos
        //public IActionResult ModificarPedidoListaCompleta(int nroPedido, Estado estadoPedido)
        //{
        //    if (_DB.modificarArchivoCadetePedido(nroPedido, estadoPedido))
        //    {
        //        return RedirectToAction("ListaCompleta");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Logger");
        //    } 
        //}
        ////modifica el pedido desde un listado filtrado de pedidos
        //public IActionResult ModificarPedidoListaFiltrada(int nroPedido, Estado estadoPedido)
        //{
        //    if (_DB.modificarArchivoCadetePedido(nroPedido, estadoPedido))
        //    {
        //        return RedirectToAction("ListaFiltrada2");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Logger");
        //    }
        //}

        //public IActionResult ListaFiltrada(DateTime fechaInicial, DateTime fechaFinal)
        //{
        //    FechaInicial = fechaInicial;
        //    FechaFinal = fechaFinal;
        //    return View(_DB.busquedaFiltrada(fechaInicial, fechaFinal));
        //}

        ////posee la misma funcion que ListaFiltrada solo que sin argumentos, tomando los datos
        ////static FechaInicial y FechaFinal de la clase Pedido
        //public IActionResult ListaFiltrada2()
        //{
        //    return View(_DB.busquedaFiltrada(FechaInicial, FechaFinal));
        //}
    }
}
