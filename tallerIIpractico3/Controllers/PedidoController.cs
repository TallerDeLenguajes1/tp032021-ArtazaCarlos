using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using tallerIIpractico3.entities;
using tallerIIpractico3.Models.Db;
using Rotativa.AspNetCore;

namespace tallerIIpractico3.Controllers
{
    public class PedidoController : Controller
    {
        private readonly DbSqlite db;
        private static DateTime fechaInicial;
        private static DateTime fechaFinal;

        public static DateTime FechaInicial { get => fechaInicial; set => fechaInicial = value; }
        public static DateTime FechaFinal { get => fechaFinal; set => fechaFinal = value; }

        public PedidoController(DbSqlite Db)
        {
            db = Db;
        }

        public IActionResult Index()
        {
            return View(db.CadeteDb.ReadCadetes());
        }

        public IActionResult ListaCompleta()
        {
            return View(db.PedidoDb.ReadPedidos());
        }

        public IActionResult ListaPedidos()
        {
            return View();
        }

        //public IActionResult crearPedido(string obs, Estado est, string nom, string dir, string tel, int idCadete)
        //{
        //    int nro = _DB.leerArchivoPedido().Count() + 1;
        //    if (_DB.guardarPedido(nro, obs, est, nom, dir, tel, idCadete))
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Logger");
        //    }
        //}



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
