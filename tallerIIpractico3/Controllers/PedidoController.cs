using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using tallerIIpractico3.entities;
using tallerIIpractico3.Models;
using Rotativa.AspNetCore;

namespace tallerIIpractico3.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly DBTemporal _DB;
        public PedidoController(ILogger<PedidoController> logger, DBTemporal DB )
        {
            _logger = logger;
            _DB = DB;
        }

        public IActionResult Index()
        {
            return View(_DB.leerArchivoCadete());
        }

        public IActionResult crearPedido(string obs, Estado est, string nom, string dir, string tel, int idCadete)
        {
            int nro = _DB.leerArchivoPedido().Count() + 1;
            if (_DB.guardarPedido(nro, obs, est, nom, dir, tel, idCadete))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Logger");
            }
        }

        public IActionResult ModificarPedido(int nroPedido, Estado estadoPedido)
        {
            if (_DB.modificarArchivoCadetePedido(nroPedido, estadoPedido))
            {
                return RedirectToAction("ListaPedidos");
            }
            else
            {
                return RedirectToAction("Index", "Logger");
            } 
        }

        public IActionResult ListaPedidos()
        {
            return View();
        }
        public IActionResult ListaCompleta()
        {
            return View(_DB.leerArchivoPedido());
        }
        public IActionResult ListaFiltrada(DateTime fechaInicial, DateTime fechaFinal)
        {
            List<Pedido> listaFiltrada = new List<Pedido>();
            List<Pedido> listaCompleta = _DB.leerArchivoPedido();
            foreach (Pedido item in listaCompleta)
            {
                if ((item.FechaHora.Date >= fechaInicial.Date) && (item.FechaHora.Date <= fechaFinal.Date) )
                {
                    listaFiltrada.Add(item);
                }
            }
            return View(listaFiltrada);
        }
    }
}
