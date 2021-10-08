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

        public IActionResult crearPedido(string obs, Estado est, int dni, string nom, string dir, string tel, int idCadete)
        {
            int nro = _DB.leerArchivoPedido().Count() + 1;
            Pedido pedido = new Pedido(nro, obs, est, dni, nom, dir, tel);
            _DB.guardarPedido(pedido, idCadete);

            return RedirectToAction("Index"); ;
        }

        public IActionResult ModificarPedido(int nroPedido, Estado estadoPedido)
        {
            _DB.modificarArchivoCadetePedido(nroPedido, estadoPedido);
            return RedirectToAction("ListaPedidos");
        }

        public IActionResult ListaPedidos()
        {
            return View(_DB.leerArchivoPedido());
        }

    }
}
