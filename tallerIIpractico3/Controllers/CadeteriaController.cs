﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using tallerIIpractico3.entities;
using tallerIIpractico3.Models;

namespace tallerIIpractico3.Controllers
{
    public class CadeteriaController : Controller
    {
        private readonly ILogger<CadeteriaController> _logger;
        private readonly DBTemporal dB;

        public CadeteriaController(ILogger<CadeteriaController> logger, DBTemporal DB)
        {
            _logger = logger;          
            dB = DB;
        }
        public IActionResult Index()
        {
            return View(dB.Cadeteria.Cadetes);
        }
    }
}