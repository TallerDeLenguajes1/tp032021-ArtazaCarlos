using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using tallerIIpractico3.entities;
using tallerIIpractico3.Models;

namespace tallerIIpractico3.Controllers
{
    public class PedidoController : Controller
    {
        static int nro = 0;
        private readonly ILogger<PedidoController> _logger;
        private readonly DBTemporal _DB;

        public PedidoController(ILogger<PedidoController> logger, DBTemporal DB )
        {
            _logger = logger;
            _DB = DB;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult crearPedido(string obs, string est, int dni, string nom, string dir, string tel)
        {
            Pedido pedido = new Pedido(nro, obs, est, dni, nom, dir, tel);
            nro++;
            _DB.guardarPedido(pedido);
            return Redirect("Index");
        }

        public IActionResult ListaPedidos()
        {
            return View(_DB.leerArchivoPedido());
        }
    }
}
