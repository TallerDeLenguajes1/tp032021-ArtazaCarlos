using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using tallerIIpractico3.entities;

namespace tallerIIpractico3.Controllers
{
    public class PedidoController : Controller
    {

        private readonly ILogger<PedidoController> _logger;
        public PedidoController(ILogger<PedidoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Pedido pedido1 = new Pedido(23, "sandwich de milanesa", "cocinando", 33443322, "carlos", "rondeau 1961", "+54878278782");
            return View(pedido1);
        }
    }
}
