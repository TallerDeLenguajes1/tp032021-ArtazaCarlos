using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using tallerIIpractico3.entities;

namespace tallerIIpractico3.Controllers
{
    public class CadeteriaController : Controller
    {
        private readonly ILogger<CadeteriaController> _logger;
        private readonly List<Cadete> cadetes;

        public CadeteriaController(ILogger<CadeteriaController> logger, List<Cadete> cadetes)
        {
            _logger = logger;
            this.cadetes = cadetes;
        }
        public IActionResult Index()
        {
            return View(cadetes);
        }
    }
}
