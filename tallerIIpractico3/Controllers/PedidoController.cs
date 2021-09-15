﻿using Microsoft.AspNetCore.Mvc;
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
        static int nro = 0;
        private readonly ILogger<PedidoController> _logger;
        private readonly List<Cadete> cadetes;

        public PedidoController(ILogger<PedidoController> logger, List<Cadete> cadetes)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            nro++;
            Pedido pedido1 = new Pedido(nro, "sandwich de milanesa", "cocinando", 33443322, "carlos", "rondeau 1961", "+54878278782");
            return View(pedido1);
        }
    }
}
